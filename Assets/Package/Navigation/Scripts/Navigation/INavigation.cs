using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Transitions;
using System;
using System.Collections.Generic;
using Zenject;

namespace Elselam.UnityRouter.Installers
{
    public interface INavigation : IInitializable
    {
        void NavigateTo<TScreen>(ITransition transitionType = null, IDictionary<string, string> parameters = null) where TScreen : IScreenInteractor;
        void NavigateTo(Type screenType, ITransition transitionType = null, IDictionary<string, string> parameters = null);
        void NavigateTo(ScreenScheme enterScheme, ITransition transition = null, bool back = false);
        void BackToLastScreen(ITransition transition = null);

        void NavigateTo(string sceneName, Action<DiContainer> extraBindings = null);
        void BackToMainScene();

        ICurrentScreen CurrentScreen { get; }
    }
}