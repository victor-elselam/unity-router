using Elselam.UnityRouter.Domain;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Elselam.UnityRouter.Installers
{
    [CreateAssetMenu(fileName = "New Screens Installer", menuName = "Elselam/UnityRouter/Installers/Area", order = 0)]
    public class AreaInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private List<ScreenRegistryObject> screens;

        public override void InstallBindings()
        {
            foreach (var screenRegistryObject in screens)
            {
                var screenRegistry = screenRegistryObject.ScreenRegistry;

                Container.BindInterfacesAndSelfTo(screenRegistry.ScreenPresenter)
                    .FromComponentInNewPrefab(screenRegistry.ScreenPrefab)
                    .AsSingle()
                    .OnInstantiated<IScreenPresenter>((context, o) => o.Disable())
                    .NonLazy();

                Container.BindInterfacesAndSelfTo(screenRegistry.ScreenInteractor)
                    .AsSingle()
                    .Lazy();

                Container.BindInterfacesAndSelfTo(screenRegistry.ScreenView)
                    .FromComponentInHierarchy()
                    .AsSingle();
            }

            Container.Bind<List<IScreenRegistry>>().FromInstance(screens
                .Select(s => (IScreenRegistry)s.ScreenRegistry).ToList())
                .AsSingle();
        }
    }
}