using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.SceneLoad;
using Elselam.UnityRouter.Url;
using UnityEngine;
using Zenject;

namespace Elselam.UnityRouter.Installers
{
    [CreateAssetMenu(fileName = "NavigationInstaller", menuName = "Elselam/UNavScreen/NavigationInstaller")]
    public class NavigationInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private AppUrlDomain appUrlDomain;
        [SerializeField] private ScreenRegistryObject defaultScreen;
        [SerializeField] private string loadingSceneName;
        [SerializeField] private string mainSceneName;

        public override void InstallBindings()
        {
            Container.Bind<IScreenRegistry>()
                .FromInstance(defaultScreen.ScreenRegistry)
                .AsSingle();

            Container.Bind<IScreenFactory>()
                .To<ScreenFactory>()
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