using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Services;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private MainMenuController _mainMenuController;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Main menu scene initialization");

            _mainMenuController = _container.Resolve<MainMenuController>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Main menu scene start");
            Debug.Log("--- WELCOME TO TYPING GAME ---");
            Debug.Log("Press '1' to start Numbers Mode");
            Debug.Log("Press '2' to start Letters Mode");
        }

        private void Update()
        {
            _mainMenuController?.ProcessInput();
        }
    }
}
