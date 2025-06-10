using UnityEngine;
using DG.Tweening;

namespace _Project.Develop.Runtime.Presentation.Pallete.Views
{
    public class PalleteView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _palleteRb;

        [Header("Настройки начальной анимации")]
        [SerializeField] private float _floorY = 0f;
        [SerializeField] private float _appearenceDuration = 3f;
        [SerializeField] private float _rotationDegrees = 360f;

        [Header("Настройки конечной анимации")]
        [SerializeField] private float _liftHeight = 5f;
        [SerializeField] private float _disappearenceDuration = 5f;
        [SerializeField] private Vector3 _rotationAmount = new Vector3(720, 720, 720);

        public void AppearPallete()
        {
            _palleteRb.isKinematic = true;

            var targetPosition = new Vector3(transform.position.x, _floorY, transform.position.z);

            var dropSequence = DOTween.Sequence();

            dropSequence.Join(
                transform.DOMove(targetPosition, _appearenceDuration).SetEase(Ease.InOutQuad)
            );

            dropSequence.Join(
                transform.DORotate(new Vector3(0, _rotationDegrees, 0), _appearenceDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutQuad)
            );

            dropSequence.OnComplete(() => _palleteRb.isKinematic = false);
        }

        public void DisappearPallete()
        {
            _palleteRb.isKinematic = true;

            var palletSequence = DOTween.Sequence();

            palletSequence.Join(
                transform.DOMoveY(transform.position.y + _liftHeight, _disappearenceDuration).SetEase(Ease.InQuad)
            );

            palletSequence.Join(
                transform.DORotate(_rotationAmount, _disappearenceDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutSine)
            );

            palletSequence.Join(
                transform.DOScale(Vector3.zero, _disappearenceDuration).SetEase(Ease.InOutQuad)
            );

            palletSequence.OnComplete(() => Destroy(transform.gameObject));
        }
    }
}