using System;
using System.Collections.Generic;
using elselam.Navigation.Domain;
using elselam.Navigation.History;
using elselam.Navigation.Navigation;
using elselam.Navigation.Transitions;
using Zenject;

namespace elselam.Navigation.Navigation {
    public interface INavigation : IInitializable {
        void NavigateTo<TScreen>(ITransition transitionType = null, IDictionary<string, string> parameters = null) where TScreen : IScreenInteractor;
        void NavigateTo(Type screenType, ITransition transitionType = null, IDictionary<string, string> parameters = null);
        void NavigateTo(ScreenScheme enterScheme, ITransition transition = null, bool back = false);
        void BackToLastScreen(ITransition transition = null);

        void NavigateTo(string sceneName, Action<DiContainer> extraBindings = null);
        void BackToMainScene();
        
        ICurrentScreen CurrentScreen { get; } 
    }
}