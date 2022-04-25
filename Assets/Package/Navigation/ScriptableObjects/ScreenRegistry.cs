using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.Extensions;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0051 // Remove unused private members
// ReSharper disable NotAccessedField.Local
namespace Elselam.UnityRouter.Installers
{
    [Serializable]
    public class ScreenRegistry : IScreenRegistry
    {
        [SerializeField] private string screenId;

        [SerializeField] private BaseScreenPresenter presenter;
        [SerializeField] private string presenterTypeName;
        private Type presenterType;

        public string ScreenId => screenId;
        public Type ScreenPresenter => presenterType ??= TypeExtensions.GetTypeByName(presenterTypeName);
        public BaseScreenPresenter ScreenPrefab => presenter;
    }
}
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore IDE0051 // Remove unused private members