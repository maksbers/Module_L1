using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Services;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("MainMenu scene services registration process");

            RegisterMainMenuController(container);
        }

        private static void RegisterMainMenuController(DIContainer container)
        {
            container.RegisterAsSingle(c => new MainMenuController(
                c.Resolve<SceneSwitcherService>(),
                c.Resolve<ICoroutinesPerformer>()
            ));
        }
    }
}
