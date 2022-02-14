using Assets.Package.Navigation.Scripts.Loader;
using Elselam.UnityRouter.Extensions;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.SceneLoad;
using Elselam.UnityRouter.ScreenLoad;
using Elselam.UnityRouter.Transitions;
using Elselam.UnityRouter.Url;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    public class UnityRouter
    {
        public static INavigation Navigation { get; internal set; }
        public static IParameterManager Parameters { get; set; }
        public static LoaderFactory LoaderFactory { get; set; }
        public static IScreenFactory ScreenFactory { get; set; }
        public static IScreenResolver ScreenResolver { get; set; }
        public static ITransition DefaultTransition { get; set; }
        public static ISceneLoader SceneLoader { get; set; }
        public static IScreenLoader ScreenLoader { get; set; }
        public static IUrlManager UrlManager { get; set; }
        public static IHistory History { get; set; }
        public static ICurrentScreen CurrentScreen { get; set; }

        private static bool setupCalled;

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
            Parameters ??= new ParameterManager();

            setupCalled = true;
        }

        /// <summary>
        /// Shortcut to allow usage of this sytem without implementing Dependency Injection.
        /// Warning! For now, you will lose support for extraBindings in SceneLoading.
        /// It's necessary to call Setup before Create.
        /// </summary>
        public static void Create()
        {
            if (!setupCalled)
                throw new InvalidOperationException("You need to setup before create");

            var navigation = new NavigationManager(LoaderFactory, ScreenResolver, UrlManager, History, CurrentScreen);
            Navigation = navigation;

            Navigation.Initialize();
        }
    }
}