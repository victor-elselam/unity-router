using System;
using elselam.Navigation.Domain;

namespace elselam.Navigation.ScriptableObjects {
    public interface IScreenRegistry {
        string ScreenId { get; }
        Type ScreenPresenter { get; }
        Type ScreenInteractor { get; }
        Type ScreenController { get; }
        BaseScreenPresenter ScreenPrefab { get; }
    }
}