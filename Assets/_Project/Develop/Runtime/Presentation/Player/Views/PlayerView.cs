using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace _Project.Develop.Runtime.Presentation.Player.Views
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Image _fadeImg;

        private float _fadeDuration;

        public void SetFadeDuration(float fadeDuration)
        {
            _fadeDuration = fadeDuration;
        }

        public void SetLookRotation(float pitch, float yaw)
        {
            _cameraTransform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
        }

        public void FadeIn()
        {
            _fadeImg.DOFade(1, _fadeDuration);
        }

        public void FadeOut()
        {
            _fadeImg.DOFade(0, _fadeDuration);
        }
    }
}