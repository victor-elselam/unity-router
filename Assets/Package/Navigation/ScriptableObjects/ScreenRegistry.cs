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

        [SerializeField] private Object interactor;
        [SerializeField] private string interactorTypeName;
        private Type interactorType;

        [SerializeField] private BaseScreenPresenter presenter;
        [SerializeField] private string presenterTypeName;
        private Type presenterType;

        [SerializeField] private Object view;
        [SerializeField] private string viewTypeName;
        private Type viewType;

        public string ScreenId => screenId;
        public Type ScreenPresenter => presenterType ??= TypeExtensions.GetTypeByName(presenterTypeName);
        public Type ScreenInteractor => interactorType ??= TypeExtensions.GetTypeByName(interactorTypeName);
        public Type ScreenView => viewType ??= TypeExtensions.GetTypeByName(viewTypeName);
        public BaseScreenPresenter ScreenPrefab => presenter;
    }
}
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore IDE0051 // Remove unused private members