using Elselam.UnityRouter.Extensions;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Url;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    public class ScreenResolver : IScreenResolver
    {
        private readonly IHistory history;
        private readonly IScreenRegistry defaultScreen;
        private readonly IUrlManager urlManager;
        private readonly IScreenFactory screenFactory;
        private readonly List<IScreenRegistry> screenRegistries;

        private string deepLinkUrl;
        private Dictionary<string, IScreenModel> screenModels;

        public ScreenResolver(
            List<IScreenRegistry> screenRegistries,
            IScreenFactory screenFactory,
            IHistory history,
            IScreenRegistry defaultScreen,
            IUrlManager urlManager)
        {
            this.screenRegistries = screenRegistries;
            this.screenFactory = screenFactory;
            this.history = history;
            this.defaultScreen = defaultScreen;
            this.urlManager = urlManager;
        }

        public void Initialize()
        {
            screenModels = new Dictionary<string, IScreenModel>();
            foreach (var screenRegistry in screenRegistries)
                screenModels[screenRegistry.ScreenId] = screenFactory.Create(screenRegistry);

            Application.deepLinkActivated += ApplicationOnDeepLinkActivated;
            if (!string.IsNullOrEmpty(Application.absoluteURL))
            {
                ApplicationOnDeepLinkActivated(Application.absoluteURL);
            }
        }

        //TODO: remove this method from interface, we need to make an adapter to Unity Application.deepLinkActivated
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

        public string GetScreenName(Type controllerType) => screenRegistries.FirstOrDefault(s => s.ScreenInteractor == controllerType)?.ScreenId;

        public IScreenModel GetScreenModel(string screenName)
        {
            if (screenName.IsNullOrEmpty())
                return null;
            return screenModels.TryGetValue(screenName, out var value) ? value : null;
        }
    }
}