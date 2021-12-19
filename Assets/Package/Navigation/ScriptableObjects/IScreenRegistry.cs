using Elselam.UnityRouter.Domain;
using System;

namespace Elselam.UnityRouter.Installers
{
    public interface IScreenRegistry
    {
        string ScreenId { get; }
        Type ScreenPresenter { get; }
        Type ScreenInteractor { get; }
        Type ScreenController { get; }
        BaseScreenPresenter ScreenPrefab { get; }
    }
}