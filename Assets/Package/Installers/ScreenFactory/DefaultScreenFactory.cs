using Elselam.UnityRouter.Domain;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Elselam.UnityRouter.Installers
{
    public class DefaultScreenFactory : IScreenFactory
    {
        private DiContainer container;

        public DefaultScreenFactory(DiContainer container)
        {
            this.container = container;
        }

        public IScreenModel Create(IScreenRegistry screenRegistry)
        {
            if (screenRegistry.ScreenPresenter.DerivesFrom<MonoBehaviour>())
                return CreateMonoBehaviour(screenRegistry);
            else
                return CreateCommon(screenRegistry);
        }

        private IScreenModel CreateMonoBehaviour(IScreenRegistry screenRegistry)
        {
            var instance = container.InstantiatePrefab(screenRegistry.ScreenPrefab)?.GetComponent<IScreenPresenter>();
            instance.Disable();

            return new ScreenModel(screenRegistry.ScreenId, instance);
        }

        private IScreenModel CreateCommon(IScreenRegistry screenRegistry)
        {
            var prefab = container.InstantiatePrefab(screenRegistry.ScreenPrefab);
            prefab.SetActive(false);

            var instance = (IScreenPresenter)container.ResolveId(screenRegistry.ScreenPresenter, screenRegistry.ScreenId);
            return new ScreenModel(screenRegistry.ScreenId, instance);
        }
    }
}