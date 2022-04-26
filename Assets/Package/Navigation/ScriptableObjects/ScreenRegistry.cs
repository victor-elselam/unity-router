using System;
using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    [Serializable]
    public class ScreenRegistry : IScreenRegistry
    {
        public ScreenRegistry(string screenId, Type presenterType, GameObject screenPrefab)
        {
            ScreenId = screenId;
            ScreenPresenter = presenterType;
            ScreenPrefab = screenPrefab;
        }

        public string ScreenId { get; }
        public Type ScreenPresenter { get; }
        public GameObject ScreenPrefab { get; }
    }
}