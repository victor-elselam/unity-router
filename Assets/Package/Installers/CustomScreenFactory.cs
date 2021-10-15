using elselam.Navigation.Domain;
using elselam.Navigation.ScriptableObjects;
using Zenject;

namespace elselam.Navigation.Installers {
    public class CustomScreenFactory : IFactory<IScreenRegistry, IScreenModel> {
        private readonly DiContainer container;

        public CustomScreenFactory(DiContainer container) {
            this.container = container;
        }
        
        public IScreenModel Create(IScreenRegistry screenRegistry) {
            
            return new ScreenModel(screenRegistry.ScreenId,
                (IScreenInteractor) container.Resolve(screenRegistry.ScreenInteractor),
                (IScreenPresenter) container.Resolve(screenRegistry.ScreenPresenter),
                (IScreenController) container.Resolve(screenRegistry.ScreenController));
        }
    }
}