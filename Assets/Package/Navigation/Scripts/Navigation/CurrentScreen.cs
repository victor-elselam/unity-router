using elselam.Navigation.Domain;
using elselam.Navigation.History;

namespace elselam.Navigation.Navigation {
    public class CurrentScreen : ICurrentScreen {
        public IScreenInteractor Screen { get; private set; }
        public ScreenScheme Scheme { get; private set; }

        public void SetCurrentScreen(IScreenInteractor screenController, ScreenScheme scheme){
            Screen = screenController;
            Scheme = scheme;
        }
    }
}