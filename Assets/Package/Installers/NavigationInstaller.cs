using elselam.Navigation.Domain;
using elselam.Navigation.History;
using elselam.Navigation.Navigation;
using elselam.Navigation.SceneLoad;
using elselam.Navigation.Url;
using UnityEngine;
using Zenject;

namespace elselam.Navigation.ScriptableObjects {
    [CreateAssetMenu(fileName = "NavigationInstaller", menuName = "Elselam/UNavScreen/NavigationInstaller")]
    public class NavigationInstaller : ScriptableObjectInstaller {
        [SerializeField] private AppUrlDomain appUrlDomain;
        [SerializeField] private ScreenRegistryObject defaultScreen;
        [SerializeField] private string loadingSceneName;
        [SerializeField] private string mainSceneName;

        public override void InstallBindings() {
            Container.Bind<IScreenRegistry>()
                     .FromInstance(defaultScreen.ScreenRegistry)
                     .AsSingle();

            Container.Bind<IUrlManager>()
                .To<UrlManager>()
                .AsSingle();
            
            Container.Bind<IScreenResolver>()
                .To<ScreenResolver>()
                .AsSingle();

            Container.Bind<ISceneLoader>()
                .To<SceneLoader>()
                .AsSingle()
                .WithArguments(loadingSceneName, mainSceneName);
            
            Container.Bind<IHistory>()
                .To<HistoryManager>()
                .AsSingle();

            Container.Bind<INavigation>()
                .To<NavigationManager>()
                .AsSingle();

            Container.Bind<IUrlDomainProvider>()
                .FromInstance(appUrlDomain)
                .AsSingle();
        }
    }
}