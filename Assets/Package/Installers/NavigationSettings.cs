using Elselam.UnityRouter.Domain;
using System;
using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    [Serializable]
    public class NavigationSettings : INavigationSettings
    {
        [SerializeField] private AppUrlDomain appUrlDomain;
        [SerializeField] private ScreenRegistryObject defaultScreen;
        [SerializeField] private string loadingSceneName; //TODO: create a scene dragger inspector
        [SerializeField] private string mainSceneName;

        public IUrlDomainProvider AppUrlDomain => appUrlDomain;
        public IScreenRegistry DefaultScreen => defaultScreen.ScreenRegistry;
        public string LoadingSceneName => loadingSceneName;
        public string MainSceneName => mainSceneName;
    }

    public interface INavigationSettings
    {
        IUrlDomainProvider AppUrlDomain { get; }
        IScreenRegistry DefaultScreen { get; }
        string LoadingSceneName { get; }
        string MainSceneName { get; }
    }
}