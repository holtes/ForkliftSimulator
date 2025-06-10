using _Project.Develop.Runtime.Core;
using _Project.Develop.Runtime.Domain.Models;
using _Project.Develop.Runtime.Presentation.Player.Views;
using UnityEngine;
using Zenject;
using R3;

namespace _Project.Develop.Runtime.Domain.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerView;

        private PlayerModel _model;
        private PlayerInputHandler _playerInput;

        [Inject]
        private void Construct(PlayerModel playerModel, PlayerInputHandler playerInput)
        {
            _model = playerModel;
            _playerInput = playerInput;
        }

        private void Awake()
        {
            _playerInput.OnLookInput
                .Subscribe(input => RotateCamera(input))
                .AddTo(this);

            Cursor.lockState = CursorLockMode.Locked;
            SetPlayerFadeDuration(_model.FadeDuration);
            ActivatePlayer();
        }

        private void SetPlayerFadeDuration(float duration)
        {
            _playerView.SetFadeDuration(duration);
        }

        private void RotateCamera(Vector2 lookInput)
        {
            var (pitch, yaw) = _model.CalculateLookRotation(lookInput);
            _playerView.SetLookRotation(pitch, yaw);
        }

        private void ActivatePlayer()
        {
            _model.SetPlayerStatus(true);
            _playerView.FadeOut();
        }
    }
}