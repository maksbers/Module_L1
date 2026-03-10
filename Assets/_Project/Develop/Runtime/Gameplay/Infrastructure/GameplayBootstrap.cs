using Assets._Project.Develop.Runtime.Gameplay.Services;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private bool _isReady;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(_container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log($"Selected mode: {_inputArgs.SelectedMode}");
            Debug.Log("Initializing Gameplay Scene");

            GameplayController gameplayController = _container.Resolve<GameplayController>();
            gameplayController.Initialize();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Start Gameplay scene");
            _isReady = true;
        }

        private void Update()
        {
            if (_isReady == false)
                return;

            GameplayController gameplayController = _container.Resolve<GameplayController>();
            gameplayController.ProcessInput();
        }
    }
}
