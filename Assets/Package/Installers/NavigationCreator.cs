using Assets.Package.Navigation.Scripts.Loader;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.SceneLoad;
using Elselam.UnityRouter.ScreenLoad;
using Elselam.UnityRouter.Transitions;
using Elselam.UnityRouter.Url;
using System.Collections.Generic;
using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    public class NavigationCreator
    {
        public static LoaderFactory LoaderFactory { get; set; }
        public static IScreenFactory ScreenFactory { get; set; }
        public static IScreenResolver ScreenResolver { get; set; }
        public static ITransition DefaultTransition { get; set; }
        public static ISceneLoader SceneLoader { get; set; }
        public static IScreenLoader ScreenLoader { get; set; }
        public static IUrlManager UrlManager { get; set; }
        public static IHistory History { get; set; }
        public static ICurrentScreen CurrentScreen { get; set; }

        /// <summary>
        /// Setup Navigation dependencies with default instances. Any earlier provided instance will be respected
        /// </summary>
        /// <param name="settings"></param>
        public static void Setup(NavigationSettings settings, List<IScreenRegistry> screenList, Transform screensContainer)
        {
            CurrentScreen ??= new CurrentScreen();
            ScreenFactory ??= new DefaultScreenFactory(screensContainer);
            History ??= new HistoryManager();
            UrlManager ??= new UrlManager(settings.AppUrlDomain, History);
            ScreenResolver ??= new ScreenResolver(screenList, ScreenFactory, History, settings.DefaultScreen, UrlManager);
            DefaultTransition ??= new DefaultTransition();
            SceneLoader ??= new DefaultSceneLoader(settings.LoadingSceneName, settings.MainSceneName);
            ScreenLoader ??= new ScreenLoader(UrlManager, ScreenResolver, DefaultTransition);
            LoaderFactory ??= new LoaderFactory(SceneLoader, ScreenLoader);
        }

        /// <summary>
        /// Shortcut to allow usage of this sytem without implementing Dependency Injection.
        /// Warning! For now, you will lose support for extraBindings in SceneLoading
        /// </summary>
        /// <param name="screenList"></param>
        /// <returns></returns>
        public static INavigation Create()
        {
            return new NavigationManager(LoaderFactory, ScreenResolver, UrlManager, History, CurrentScreen);
        }
    }
}