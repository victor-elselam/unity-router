using Elselam.UnityRouter.Domain;
using System;
using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    [Serializable]
    public class NavigationSettings : INavigationSettings
    {
        [SerializeField] private AppUrlDomain appUrlDomain;
        [SerializeField] private string loadingSceneName; //TODO: create a scene dragger inspector
        [SerializeField] private string mainSceneName;
        [SerializeField] private bool createByDemand;

        public IUrlDomainProvider AppUrlDomain => appUrlDomain;
        public string LoadingSceneName => loadingSceneName;
        public string MainSceneName => mainSceneName;
        public bool CreateByDemand => createByDemand;
    }

    public interface INavigationSettings
    {
        IUrlDomainProvider AppUrlDomain { get; }
        string LoadingSceneName { get; }
        string MainSceneName { get; }
        bool CreateByDemand { get; }
    }
}