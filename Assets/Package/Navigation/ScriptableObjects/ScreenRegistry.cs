using System;
using elselam.Navigation.Domain;
using UnityEngine;
using Object = UnityEngine.Object;
using elselam.Navigation.Extensions;

// ReSharper disable NotAccessedField.Local
namespace elselam.Navigation.ScriptableObjects {
    [Serializable]
    public class ScreenRegistry : IScreenRegistry {
        [SerializeField] private string screenId;

        [SerializeField] private Object interactor;
        [SerializeField] private string interactorTypeName;
        private Type interactorType;

        [SerializeField] private BaseScreenPresenter presenter;
        [SerializeField] private string presenterTypeName;
        private Type presenterType;

        [SerializeField] private Object controller;
        [SerializeField] private string controllerTypeName;
        private Type controllerType;

        public string ScreenId => screenId;
        public Type ScreenPresenter => presenterType ??= TypeExtensions.GetTypeByName(presenterTypeName);
        public Type ScreenInteractor => interactorType ??= TypeExtensions.GetTypeByName(interactorTypeName);
        public Type ScreenController => controllerType ??= TypeExtensions.GetTypeByName(controllerTypeName);
        public BaseScreenPresenter ScreenPrefab => presenter;
    }
}