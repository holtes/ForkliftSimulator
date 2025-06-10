using UnityEngine;
using UnityEngine.InputSystem;
using R3;

namespace _Project.Develop.Runtime.Core
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }

        public Observable<Vector2> OnMoveInput => _onMoveInput;
        public Observable<Vector2> OnLookInput => _onLookInput;
        public Observable<Unit> OnMotorLaunchInput => _onMotorLaunchInput;
        public Observable<float> OnLiftForkInput => _onLiftForkInput;

        private Subject<Vector2> _onMoveInput = new();
        private Subject<Vector2> _onLookInput = new();
        private Subject<Unit> _onMotorLaunchInput = new();
        private Subject<float> _onLiftForkInput = new();

        public void OnMove(InputValue value)
        {
            MoveInput = value.Get<Vector2>();
            _onMoveInput.OnNext(MoveInput);
        }

        public void OnLook(InputValue value)
        {
            LookInput = value.Get<Vector2>();
            _onLookInput.OnNext(LookInput);
        }

        public void OnMotorLaunch()
        {
            _onMotorLaunchInput.OnNext(Unit.Default);
        }

        public void OnLiftFork(InputValue value)
        {
            _onLiftForkInput.OnNext(value.Get<float>());
        }
    }
}