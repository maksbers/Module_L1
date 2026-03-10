using Assets._Project.Develop.Runtime.Gameplay.Services;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            Debug.Log("Gameplay scene services registration process");

            container.RegisterAsSingle(c => new SequenceGeneratorService());
            container.RegisterAsSingle(c => new GameplayController(
                c.Resolve<ConfigsProviderService>(),
                c.Resolve<SequenceGeneratorService>(),
                c,
                args));
        }
    }
}
