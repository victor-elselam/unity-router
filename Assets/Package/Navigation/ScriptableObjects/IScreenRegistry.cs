using Elselam.UnityRouter.Domain;
using System;
using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    public interface IScreenRegistry
    {
        string ScreenId { get; }
        Type ScreenPresenter { get; }
        GameObject ScreenPrefab { get; }
    }
}