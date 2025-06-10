using _Project.Develop.Runtime.Data.Configs;
using UnityEngine;

namespace _Project.Develop.Runtime.Domain.Models
{
    public class PlayerModel
    {
        private readonly float _lookSensitivity;
        private readonly float _minYaw;
        private readonly float _maxYaw;
        private readonly float _minPitch;
        private readonly float _maxPitch;

        public bool IsActive { get; private set; }
        public float FadeDuration { get; private set; }

        private float _yaw;
        private float _pitch;

        public PlayerModel(GameConfig gameConfig)
        {
            _lookSensitivity = gameConfig.LookSensivity;
            _minPitch = gameConfig.MinPitch;
            _maxPitch = gameConfig.MaxPitch;
            _minYaw = gameConfig.MinYaw;
            _maxYaw = gameConfig.MaxYaw;

            FadeDuration = gameConfig.AppearencePlayerDuration;
        }

        public void SetPlayerStatus(bool state)
        {
            IsActive = state;
        }

        public (float pitch, float yaw) CalculateLookRotation(Vector2 lookInput)
        {
            _yaw += lookInput.x * _lookSensitivity;
            _pitch -= lookInput.y * _lookSensitivity;

            _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch);
            _yaw = Mathf.Clamp(_yaw, _minYaw, _maxYaw);

            return (_pitch, _yaw);
        }
    }
}