using UnityEngine;
using R3;
using R3.Triggers;
using DG.Tweening;


namespace _Project.Develop.Runtime.Presentation.Forklift.Views
{
    public class WheelView : MonoBehaviour
    {
        [SerializeField] private Transform _wheel;

        private float _rotationSpeed;

        private Quaternion _initialRotation;
        private Tween _returnTween;

        private void Awake()
        {
            _initialRotation = _wheel.rotation;

            this.UpdateAsObservable()
                .Subscribe(_ => RotateWheel())
                .AddTo(this);
        }

        public void SetRotationSpeed(float rotationSpeed)
        {
            _rotationSpeed = rotationSpeed;

            if (Mathf.Approximately(_rotationSpeed, 0f)) StartReturnToCenter();
            else _returnTween?.Kill();
        }

        private void RotateWheel()
        {
            if (!Mathf.Approximately(_rotationSpeed, 0f))
            {
                _wheel.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime, Space.Self);
            }
        }

        private void StartReturnToCenter()
        {
            _returnTween?.Kill();
            _returnTween = _wheel.DOLocalRotateQuaternion(_initialRotation, 0.5f)
                .SetEase(Ease.OutCubic);
        }
    }
}