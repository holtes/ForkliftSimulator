using UnityEngine;

namespace _Project.Develop.Runtime.Data.Configs
{
    [CreateAssetMenu(fileName = "New GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Настройки игрока")]
        [SerializeField] private float _lookSensitivity = 2f;
        [SerializeField] private float _appearencePlayerDuration = 3f;
        [SerializeField] private float _minPitch = -30f;
        [SerializeField] private float _maxPitch = 60f;
        [SerializeField] private float _minYaw = -240;
        [SerializeField] private float _maxYaw = -120;
        public float LookSensivity => _lookSensitivity;
        public float AppearencePlayerDuration => _appearencePlayerDuration;
        public float MinPitch => _minPitch;
        public float MaxPitch => _maxPitch;
        public float MinYaw => _minYaw;
        public float MaxYaw => _maxYaw;
    }
}