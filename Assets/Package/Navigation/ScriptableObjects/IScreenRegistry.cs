using Elselam.UnityRouter.Domain;
using System;

namespace Elselam.UnityRouter.Installers
{
    public interface IScreenRegistry
    {
        string ScreenId { get; }
        Type ScreenPresenter { get; }
        BaseScreenPresenter ScreenPrefab { get; }
    }
}