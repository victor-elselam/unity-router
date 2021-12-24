using Elselam.UnityRouter.History;
using Elselam.UnityRouter.SceneLoad;
using Elselam.UnityRouter.Url;
using System.Collections.Generic;
using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    public class NavigationCreator
    {
        public static IScreenFactory ScreenFactory { get; set; }
        public static IScreenResolver ScreenResolver { get; set; }
        public static ISceneLoader SceneLoader { get; set; }
        public static IUrlManager UrlManager { get; set; }
        public static IHistory History { get; set; }
        public static ICurrentScreen CurrentScreen { get; set; }

        /// <summary>
        /// Setup Navigation dependencies with default instances. Any earlier provided instance will be respected
        /// </summary>
        /// <param name="settings"></param>
        public static void Setup(NavigationSettings settings, Transform screensContainer)
        {
            CurrentScreen ??= new CurrentScreen();
            ScreenFactory ??= new DefaultScreenFactory(screensContainer);
            History ??= new HistoryManager();
            UrlManager ??= new UrlManager(settings.AppUrlDomain, History);
            ScreenResolver ??= new ScreenResolver(History, settings.DefaultScreen.ScreenRegistry, UrlManager);
            SceneLoader ??= new DISceneLoader(settings.LoadingSceneName, settings.MainSceneName, null);
        }

        /// <summary>
        /// Shortcut to allow usage of this sytem without implementing Dependency Injection.
        /// Warning! For now, you will lose support for extraBindings in SceneLoading
        /// </summary>
        /// <param name="screenList"></param>
        /// <returns></returns>
        public static INavigation Create(List<IScreenRegistry> screenList)
        {
            return new NavigationManager(screenList, ScreenFactory, ScreenResolver, SceneLoader, UrlManager, History, CurrentScreen);
        }
    }
}