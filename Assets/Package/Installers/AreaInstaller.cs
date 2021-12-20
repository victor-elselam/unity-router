using System.Collections.Generic;
using System.Linq;
using elselam.Navigation.Domain;
using elselam.Navigation.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace elselam.Navigation.Installers {
    [CreateAssetMenu(fileName = "New Screens Installer", menuName = "Elselam/UNavScreen/ScreensInstaller", order = 0)]
    public class AreaInstaller : ScriptableObjectInstaller {
        [SerializeField] private List<ScreenRegistryObject> screens;

        public override void InstallBindings() {
            foreach (var screenRegistryObject in screens) {
                var screenRegistry = screenRegistryObject.ScreenRegistry;

                Container.BindInterfacesAndSelfTo(screenRegistry.ScreenPresenter)
                    .FromComponentInNewPrefab(screenRegistry.ScreenPrefab)
                    .AsSingle()
                    .OnInstantiated<IScreenPresenter>((context, o) => o.Disable())
                    .NonLazy();

                Container.BindInterfacesAndSelfTo(screenRegistry.ScreenInteractor)
                    .AsSingle()
                    .Lazy();
                
                Container.BindInterfacesAndSelfTo(screenRegistry.ScreenController)
                    .FromComponentInHierarchy()
                    .AsSingle();
            }

            Container.BindFactory<IScreenRegistry, IScreenModel, ScreenFactory>()
                .FromFactory<CustomScreenFactory>();

            Container.Bind<List<IScreenRegistry>>().FromInstance(screens
                .Select(s => (IScreenRegistry) s.ScreenRegistry).ToList())
                .AsSingle();
        }

    }
}