using Elselam.UnityRouter.Domain;
using System;

namespace Elselam.UnityRouter.Installers
{
    [Serializable]
    public class NavigationSettings
    {
        public AppUrlDomain AppUrlDomain;
        public ScreenRegistryObject DefaultScreen;
        public string LoadingSceneName;
        public string MainSceneName;
    }
}