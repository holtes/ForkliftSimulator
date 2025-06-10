using _Project.Develop.Runtime.Core;
using _Project.Develop.Runtime.Data.Configs;
using _Project.Develop.Runtime.Domain.Models;
using UnityEngine;
using Zenject;

namespace _Project.Develop.Runtime.Bootstrap.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInputHandler _inputHandler;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private ForkliftConfig _forkliftConfig;

        public override void InstallBindings()
        {
            InstallPlayerInputHandler();
            InstallForkLiftModel();
            InstallForkModel();
            InstallPlayerModel();
            InstallPalleteModel();
        }

        private void InstallPlayerInputHandler()
        {
            Container
                .Bind<PlayerInputHandler>()
                .FromInstance(_inputHandler);
        }

        private void InstallForkLiftModel()
        {
            Container
                .Bind<ForkliftModel>()
                .AsSingle()
                .WithArguments(_forkliftConfig);
        }

        private void InstallForkModel()
        {
            Container
                .Bind<ForkModel>()
                .AsSingle()
                .WithArguments(_forkliftConfig);
        }

        private void InstallPlayerModel()
        {
            Container
                .Bind<PlayerModel>()
                .AsSingle()
                .WithArguments(_gameConfig);
        }

        private void InstallPalleteModel()
        {
            Container
                .Bind<PalleteModel>()
                .AsSingle();
        }
    }
}