using _Project.Develop.Runtime.Core;
using _Project.Develop.Runtime.Domain.Models;
using _Project.Develop.Runtime.Presentation.Fork.Views;
using UnityEngine;
using Zenject;
using R3;

namespace _Project.Develop.Runtime.Domain.Controllers
{
    public class ForkController : MonoBehaviour
    {
        [SerializeField] private ForkView _forkView;

        private ForkModel _model;
        private PlayerInputHandler _playerInput;

        [Inject]
        public void Construct(ForkModel forkModel, PlayerInputHandler playerInput)
        {
            _model = forkModel;
            _playerInput = playerInput;
        }

        private void Awake()
        {
            _playerInput.OnLiftForkInput
                .Subscribe(input => LiftFork(input))
                .AddTo(this);
        }

        private void LiftFork(float forkInput)
        {
            var forkSpeed = _model.CalculateLiftSpeed(forkInput);
            _forkView.SetLiftSpeed(forkSpeed);
        }
    }
}