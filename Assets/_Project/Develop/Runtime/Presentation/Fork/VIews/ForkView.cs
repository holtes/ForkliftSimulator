using System.Collections.Generic;
using UnityEngine;
using R3;
using R3.Triggers;

namespace _Project.Develop.Runtime.Presentation.Fork.Views
{
    public class ForkView : MonoBehaviour
    {
        [SerializeField] private ForkGrabberView _forkGrabberView;
        [SerializeField] private Rigidbody _liftRb;
        [SerializeField] private Transform _railTransform;
        [SerializeField] private float _minForkHeight = 0f;
        [SerializeField] private float _maxForkHeight = 1.5f;

        private float _liftSpeed;
        private float _currentHeight;

        private readonly HashSet<Rigidbody> _palletsInContact = new();
        private readonly Dictionary<Rigidbody, FixedJoint> _activeJoints = new();

        private void Awake()
        {
            this.FixedUpdateAsObservable()
                .Subscribe(_ => MoveFork())
                .AddTo(this);

            _forkGrabberView.OnCollisionEnterEvent
                .Subscribe(rb => _palletsInContact.Add(rb))
                .AddTo(this);
            _forkGrabberView.OnCollisionExitEvent
                .Subscribe(rb => _palletsInContact.Remove(rb))
                .AddTo(this);

            _currentHeight = _liftRb.transform.localPosition.y;
        }

        public void SetLiftSpeed(float speed)
        {
            _liftSpeed = speed;
        }

        private void MoveFork()
        {
            _currentHeight += _liftSpeed * Time.fixedDeltaTime;
            _currentHeight = Mathf.Clamp(_currentHeight, _minForkHeight, _maxForkHeight);

            var currentHeightWorld = new Vector3(0f, 0f, _currentHeight);
            currentHeightWorld = _railTransform.TransformPoint(currentHeightWorld);

            var railPos = _railTransform.position;
            var targetWorldPos = new Vector3(railPos.x, currentHeightWorld.y, railPos.z);

            var currentRot = _liftRb.rotation;
            var targetRot = _railTransform.rotation;

            var rotationSpeed = 150;
            var smoothedRot = Quaternion.RotateTowards(currentRot, targetRot, rotationSpeed * Time.fixedDeltaTime);

            _liftRb.MovePosition(targetWorldPos);
            _liftRb.MoveRotation(smoothedRot);

            HandlePalletGrabbing();
        }

        private void HandlePalletGrabbing()
        {
            float threshold = _maxForkHeight * 0.5f;

            foreach (var pallet in _palletsInContact)
            {
                var alreadyGrabbed = _activeJoints.ContainsKey(pallet);

                if (_currentHeight >= threshold && !alreadyGrabbed)
                {
                    var joint = pallet.gameObject.AddComponent<FixedJoint>();
                    joint.connectedBody = _liftRb;
                    joint.breakForce = Mathf.Infinity;
                    joint.breakTorque = Mathf.Infinity;

                    _activeJoints[pallet] = joint;
                }
                else if (_currentHeight < threshold && alreadyGrabbed)
                {
                    Destroy(_activeJoints[pallet]);
                    _activeJoints.Remove(pallet);
                }
            }
        }


    }
}