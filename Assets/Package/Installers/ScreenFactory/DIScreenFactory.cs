using Elselam.UnityRouter.Domain;
using System.Collections.Generic;
using Zenject;

namespace Elselam.UnityRouter.Installers
{
    public class DIScreenFactory : IScreenFactory
    {
        private readonly DiContainer container;

        public DIScreenFactory(DiContainer container)
        {
            this.container = container;
        }

        public IScreenModel Create(IScreenRegistry screenRegistry)
        {
            return new ScreenModel(screenRegistry.ScreenId,
                (IScreenInteractor)container.Resolve(screenRegistry.ScreenInteractor),
                (IScreenPresenter)container.Resolve(screenRegistry.ScreenPresenter),
                (IScreenView)container.Resolve(screenRegistry.ScreenView));
        }
    }
}