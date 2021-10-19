
using elselam.Navigation.Domain;
using elselam.Navigation.History;

namespace elselam.Navigation.Navigation {
    public interface ICurrentScreen {
        IScreenInteractor Screen { get; }
        ScreenScheme Scheme { get; }
        void SetCurrentScreen(IScreenInteractor screenController, ScreenScheme scheme); 
    }
}