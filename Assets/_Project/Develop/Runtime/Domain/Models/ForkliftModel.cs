using _Project.Develop.Runtime.Data.Configs;

namespace _Project.Develop.Runtime.Domain.Models
{
    public class ForkliftModel
    {
        public readonly int AccelerationMultiplier;
        public readonly int DecelerationMultiplier;
        public readonly int MaxSteeringAngle;
        public readonly float SteeringSpeed;
        public readonly int DriftMultiplier;
        public readonly int SecondsForFuelWaste;
        public readonly int ReduceSpeedModifier;
        public readonly int FuelWastePercents;

        public bool IsMotorLaunched { get; private set; }

        private readonly int _defaultMaxSpeed;
        private int _maxSpeed;
        private int _fuel = 100;

        public int Fuel
        {
            get => _fuel > 0 ? _fuel : 0;
            private set => _fuel = value;
        }
        public int CurrentMaxSpeed
        {
            get => IsMotorLaunched ? _maxSpeed : 0;
            private set => _maxSpeed = value;
        }


        public ForkliftModel(ForkliftConfig forkliftConfig)
        {
            _defaultMaxSpeed = forkliftConfig.MaxSpeed;
            _maxSpeed = forkliftConfig.MaxSpeed;
            AccelerationMultiplier = forkliftConfig.AccelerationMultiplier;
            DecelerationMultiplier = forkliftConfig.DecelerationMultiplier;
            MaxSteeringAngle = forkliftConfig.MaxSteeringAngle;
            SteeringSpeed = forkliftConfig.SteeringSpeed;
            DriftMultiplier = forkliftConfig.DriftMultiplier;

            SecondsForFuelWaste = forkliftConfig.SeocndsForFuelWaste;
            ReduceSpeedModifier = forkliftConfig.ReduceSpeedModifier;
            FuelWastePercents = forkliftConfig.FuelWastePercents;
        }

        public void ConsumeFuel(int amount)
        {
            _fuel -= amount;
            if (_fuel <= 0) IsMotorLaunched = false; 
        }

        public void Refuel(int amount)
        {
            _fuel = amount;
        }

        public void IncreaseSpeed(int modifier)
        {
            _maxSpeed *= modifier;
        }

        public void ReduceSpeed(int modifier)
        {
            _maxSpeed /= modifier;
        }

        public void ResetSpeed()
        {
            _maxSpeed = _defaultMaxSpeed;
        }

        public void SetMotorLaunchState(bool state)
        {
            if (_fuel <= 0) return;
            IsMotorLaunched = state;
        }

        public void StopMotor()
        {
            SetMotorLaunchState(false);
        }

        public void StartMotor()
        {
            SetMotorLaunchState(true);
        }

        public float CalculateWheelRotationSpeed(float turnInput)
        {
            return MaxSteeringAngle * turnInput;
        }
    }
}