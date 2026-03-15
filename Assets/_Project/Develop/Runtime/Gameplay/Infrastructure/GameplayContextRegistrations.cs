using Assets._Project.Develop.Runtime.Gameplay.Services;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            Debug.Log("Gameplay scene services registration process");

            RegisterSequenceGeneratorService(container);
            RegisterGameplayController(container, args);
        }

        private static void RegisterSequenceGeneratorService(DIContainer container)
        {
            container.RegisterAsSingle(c => new SequenceGeneratorService());
        }

        private static void RegisterGameplayController(DIContainer container, GameplayInputArgs args)
        {
            container.RegisterAsSingle(c => new GameplayController(
                c.Resolve<ConfigsProviderService>(),
                c.Resolve<SequenceGeneratorService>(),
                c.Resolve<SceneSwitcherService>(),
                c.Resolve<ICoroutinesPerformer>(),
                args));
        }
    }
}
