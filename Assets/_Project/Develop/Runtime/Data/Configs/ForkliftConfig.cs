using UnityEngine;

namespace _Project.Develop.Runtime.Data.Configs
{
    [CreateAssetMenu(fileName = "New ForkliftConfig", menuName = "Configs/ForkliftConfig")]
    public class ForkliftConfig : ScriptableObject
    {
        [Header("��������� ����������")]
        [SerializeField] private int _maxSpeed = 20;
        [SerializeField] private int _accelerationMultiplier = 1;
        [SerializeField] private int _decelerationMultiplier = 3;
        [SerializeField] private int _maxSteeringAngle = 35;
        [SerializeField] private float _steeringSpeed = 0.3f;
        [SerializeField] private int _driftMultiplier = 3;
        

        [Header("��������� ������� �������")]
        [SerializeField] private int _seocndsForFuelWaste = 10;
        [SerializeField] private int _reduceSpeedModifier = 2;
        [SerializeField] private int _fuelWastePercents = 1;

        [Header("��������� �����")]
        [SerializeField] private float _forkLiftSpeed;

        public int MaxSpeed => _maxSpeed;
        public int AccelerationMultiplier => _accelerationMultiplier;
        public int DecelerationMultiplier => _decelerationMultiplier;
        public int MaxSteeringAngle => _maxSteeringAngle;
        public float SteeringSpeed => _steeringSpeed;
        public int DriftMultiplier => _driftMultiplier;

        public int SeocndsForFuelWaste => _seocndsForFuelWaste;
        public int ReduceSpeedModifier => _reduceSpeedModifier;
        public int FuelWastePercents => _fuelWastePercents;

        public float ForkLiftSpeed => _forkLiftSpeed;
    }
}