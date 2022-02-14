using Assets.Package.Navigation.Scripts.Loader;
using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.Extensions;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.SceneLoad;
using Elselam.UnityRouter.ScreenLoad;
using Elselam.UnityRouter.Transitions;
using Elselam.UnityRouter.Url;
using UnityEngine;
using Zenject;

namespace Elselam.UnityRouter.Installers
{
    [CreateAssetMenu(fileName = "NavigationInstaller", menuName = "Elselam/UnityRouter/Installers/Navigation")]
    public class NavigationInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private NavigationSettings navigationSettings;

        public override void InstallBindings()
        {
            Container.Bind<IScreenRegistry>()
                .FromInstance(navigationSettings.DefaultScreen)
                .AsSingle();

            Container.Bind<ILoaderFactory>()
                .To<LoaderFactory>()
                .AsSingle();

            Container.Bind<IScreenFactory>()
                .To<DIScreenFactory>()
                .AsSingle();

            Container.Bind<IUrlManager>()
                .To<UrlManager>()
                .AsSingle();

            Container.Bind<IScreenResolver>()
                .To<ScreenResolver>()
                .AsSingle();

            Container.Bind<ISceneLoader>()
                .To<DISceneLoader>()
                .AsSingle()
                .WithArguments(navigationSettings.LoadingSceneName, navigationSettings.MainSceneName);

            Container.Bind<ITransition>()
                .To<DefaultTransition>()
                .AsSingle();

            Container.Bind<IScreenLoader>()
                .To<ScreenLoader>()
                .AsSingle();

            Container.Bind<IHistory>()
                .To<HistoryManager>()
                .AsSingle();

            Container.Bind<INavigation>()
                .To<NavigationManager>()
                .AsSingle();

            Container.Bind<IUrlDomainProvider>()
                .FromInstance(navigationSettings.AppUrlDomain)
                .AsSingle();

            Container.Bind<ICurrentScreen>()
                .To<CurrentScreen>()
                .AsSingle();

            Container.Bind<IParameterManager>()
                .To<ParameterManager>()
                .AsSingle();
        }
    }
}