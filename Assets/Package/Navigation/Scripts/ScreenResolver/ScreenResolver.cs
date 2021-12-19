using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Installers;
using Elselam.UnityRouter.Url;
using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    public class ScreenResolver : IScreenResolver
    {
        private readonly IHistory history;
        private readonly IScreenRegistry defaultScreen;
        private readonly IUrlManager urlManager;

        private ScreenScheme screenScheme;
        private string deepLinkUrl;

        public ScreenResolver(IHistory history,
            IScreenRegistry defaultScreen,
            IUrlManager urlManager)
        {
            this.history = history;
            this.defaultScreen = defaultScreen;
            this.urlManager = urlManager;
        }

        public void Initialize()
        {
            Application.deepLinkActivated += ApplicationOnDeepLinkActivated;
            if (!string.IsNullOrEmpty(Application.absoluteURL))
            {
                ApplicationOnDeepLinkActivated(Application.absoluteURL);
            }
        }

        public void ApplicationOnDeepLinkActivated(string deepLinkUrl) => this.deepLinkUrl = deepLinkUrl;

        public ScreenScheme ResolveScheme()
        {
            if (!string.IsNullOrEmpty(deepLinkUrl))
            {
                var scheme = urlManager.Deserialize(deepLinkUrl);
                deepLinkUrl = null;
                return scheme;
            }
            return history.HasHistory ? history.Back() : urlManager.BuildToScheme(defaultScreen.ScreenId, null);
        }
    }
}