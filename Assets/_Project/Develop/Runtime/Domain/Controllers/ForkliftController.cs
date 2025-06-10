using _Project.Develop.Runtime.Core;
using _Project.Develop.Runtime.Domain.Models;
using _Project.Develop.Runtime.Presentation.Forklift.Views;
using UnityEngine;
using System;
using Zenject;
using R3;

namespace _Project.Develop.Runtime.Domain.Controllers
{
    public class ForkliftController : MonoBehaviour
    {
        [SerializeField] private WheelView _wheelView;
        [SerializeField] private FuelTabView _fuelTabView;
        [SerializeField] private PrometeoCarController _promCarController;

        private ForkliftModel _model;
        private PlayerInputHandler _playerInput;

        private IDisposable _fuelConsumptionInterval;
        private bool _isSpeedReduced = false;

        [Inject]
        private void Construct(ForkliftModel forkliftModel, PlayerInputHandler playerInput)
        {
            _model = forkliftModel;
            _playerInput = playerInput;
        }

        private void Awake()
        {
            _playerInput.OnMoveInput
                .Subscribe(input => MoveForkLift(input))
                .AddTo(this);
            _playerInput.OnMoveInput
                .Subscribe(input => RotateWheel(input))
                .AddTo(this);
            _playerInput.OnMotorLaunchInput
                .Subscribe(_ => ControlMotor())
                .AddTo(this);

            InitPCC();
        }

        private void InitPCC()
        {
            _promCarController.maxSpeed = _model.CurrentMaxSpeed;
            _promCarController.maxReverseSpeed = _model.CurrentMaxSpeed / 2;
            _promCarController.accelerationMultiplier = _model.AccelerationMultiplier;
            _promCarController.decelerationMultiplier = _model.DecelerationMultiplier;
            _promCarController.maxSteeringAngle = _model.MaxSteeringAngle;
            _promCarController.steeringSpeed = _model.SteeringSpeed;
            _promCarController.handbrakeDriftMultiplier = _model.DriftMultiplier;
        }

        private void SetForkliftSpeed(int speed)
        {
            _promCarController.maxSpeed = speed;
            _promCarController.maxReverseSpeed = speed / 2;
        }

        private void ReduceSpeed(int modifier)
        {
            _model.ReduceSpeed(modifier);
            SetForkliftSpeed(_model.CurrentMaxSpeed);
        }

        private void ControlMotor()
        {
            _model.SetMotorLaunchState(!_model.IsMotorLaunched);

            if (_model.IsMotorLaunched)
            {
                StartForklift();
                _fuelConsumptionInterval = 
                    Observable
                    .Interval(TimeSpan.FromSeconds(_model.SecondsForFuelWaste))
                    .Subscribe(_ => ConsumeFuel())
                    .AddTo(this);
            }
            else StopForkLift();
        }

        private void ConsumeFuel()
        {
            _model.ConsumeFuel(_model.FuelWastePercents);
            SetFuelProgress(_model.Fuel);

            if (_model.Fuel <= 0) StopForkLift();
            else if (_model.Fuel < 50 && !_isSpeedReduced)
            {
                ReduceSpeed(_model.ReduceSpeedModifier);
                _isSpeedReduced = true;
            }
        }

        public void Refuel()
        {
            _model.Refuel(100);
            if (_model.Fuel > 50) _model.ResetSpeed();
            _isSpeedReduced = false;
            SetForkliftSpeed(_model.CurrentMaxSpeed);
        }

        private void SetFuelProgress(int progress)
        {
            _fuelTabView.SetProgress(progress);
        }

        private void StartForklift()
        {
            _isSpeedReduced = _model.Fuel < 50;
            if (_isSpeedReduced) ReduceSpeed(_model.ReduceSpeedModifier);
            else SetForkliftSpeed(_model.CurrentMaxSpeed);
        }

        private void StopForkLift()
        {
            SetForkliftSpeed(_model.CurrentMaxSpeed);
            CancelFuelConsumption();
        }

        private void MoveForkLift(Vector2 moveInput)
        {
            var vertical = moveInput.y;
            var horizontal = moveInput.x;

            if (vertical > 0.01f) _promCarController.GoForward();
            else if (vertical < -0.01f) _promCarController.GoReverse();
            else _promCarController.DecelerateCar();

            if (horizontal < -0.01f) _promCarController.TurnLeft();
            else if (horizontal > 0.01f) _promCarController.TurnRight();
            else _promCarController.ResetSteeringAngle();
        }

        private void RotateWheel(Vector2 moveInput)
        {
            var rotationSpeed = _model.CalculateWheelRotationSpeed(moveInput.x);
            _wheelView.SetRotationSpeed(rotationSpeed);
        }

        public void CancelFuelConsumption()
        {
            if (_fuelConsumptionInterval != null)
            {
                _fuelConsumptionInterval.Dispose();
                _fuelConsumptionInterval = null;
            }
        }
    }
}