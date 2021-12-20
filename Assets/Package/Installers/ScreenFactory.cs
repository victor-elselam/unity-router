using Elselam.UnityRouter.Domain;
using Zenject;

namespace Elselam.UnityRouter.Installers
{
    public class ScreenFactory : IScreenFactory
    {
        private readonly DiContainer container;

        public ScreenFactory(DiContainer container)
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