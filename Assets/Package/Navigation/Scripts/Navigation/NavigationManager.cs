using Assets.Package.Navigation.Scripts.Loader;
using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Transitions;
using Elselam.UnityRouter.Url;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Elselam.UnityRouter.Installers
{
    public class NavigationManager : INavigation
    {
        private readonly ILoaderFactory loaderFactory;
        private readonly IHistory history;
        private readonly IUrlManager urlManager;
        private readonly ICurrentScreen currentScreen;
        private readonly IScreenResolver screenResolver;

        private bool loading;

        [Inject]
        public NavigationManager(
            ILoaderFactory loaderFactory,
            IScreenResolver screenResolver,
            IUrlManager urlManager,
            IHistory history,
            ICurrentScreen currentScreen)
        {
            this.loaderFactory = loaderFactory;
            this.history = history;
            this.screenResolver = screenResolver;
            this.urlManager = urlManager;
            this.currentScreen = currentScreen;
        }

        public void Initialize() => screenResolver.Initialize();

        public void BackToLastScreen(ITransition transition = null)
        {
            var scheme = history.Back();
            if (scheme == null)
                throw new NavigationException($"{nameof(scheme)} cannot be null");

            NavigateTo(scheme, transition, true);
        }

        public void NavigateTo<TScreen>(ITransition transition = null, IDictionary<string, string> parameters = null) where TScreen : IScreenInteractor
            => NavigateTo(typeof(TScreen), transition, parameters);

        public void NavigateTo(Type screenType, ITransition transition = null, IDictionary<string, string> parameters = null)
        {
            var screenName = screenResolver.GetScreenName(screenType);
            if (string.IsNullOrEmpty(screenName))
                throw new NavigationException($"Screen Registry of type: {screenType} needs to be registered");

            NavigateTo(urlManager.BuildToScheme(screenName, parameters), transition);
        }

        public void NavigateTo(string screenName, ITransition transition = null, IDictionary<string, string> parameters = null) => 
            NavigateTo(new ScreenScheme("", screenName, parameters), transition, false);

        public void NavigateTo(ScreenScheme enterScheme, ITransition transition = null) => NavigateTo(enterScheme, transition, false);
        public void NavigateToDefaultScreen() => NavigateTo(screenResolver.ResolveFirstScreen(), null, false);

        private async void NavigateTo(ScreenScheme enterScheme, ITransition transition = null, bool back = false)
        {
            if (loading) //safe guard to avoid concurrent loadings
                return;
            loading = true;

            //TODO: add a logger interface, so users can use their own logger and get info about navigations
            Debug.Log($"UnityRouter - Going from {currentScreen.Scheme?.ScreenId ?? "null"}, to {enterScheme.ScreenId}");

            var loader = loaderFactory.GetLoader(enterScheme.ScreenId, currentScreen.Scheme?.ScreenId);
            var exitScheme = await loader.Load(enterScheme, currentScreen.Scheme, transition, back);

            if (!back && exitScheme != null)
                history.Add(exitScheme);

            currentScreen.SetCurrentScreen(enterScheme);

            loading = false;
        }
    }
}