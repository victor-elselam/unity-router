using Elselam.UnityRouter.Extensions;
using System;
using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    [Serializable]
    public class ScreenRegistry : IScreenRegistry
    {
        public ScreenRegistry(string screenId, Type presenterType, GameObject screenPrefab)
        {
            this.screenId = screenId;
            this.screenPresenterName = presenterType.Name;
            this.screenPrefab = screenPrefab;
        }

        public string ScreenId => screenId;
        public Type ScreenPresenter => TypeExtensions.GetTypeByName(screenPresenterName);
        public GameObject ScreenPrefab => screenPrefab;


        [SerializeField] private string screenId;
        [SerializeField] private UnityEngine.Object screenPresenter;
        [SerializeField] private GameObject screenPrefab;
        [SerializeField] private string screenPresenterName;
    }
}