
using elselam.Navigation.Domain;

namespace elselam.Navigation.ScriptableObjects {

    public class ScreenModel : IScreenModel {
        public string ScreenId { get; }
        public IScreenInteractor Interactor { get; }
        public IScreenPresenter Presenter { get; }
        public IScreenController ViewController { get; }
        
        public ScreenModel(string screenId, IScreenInteractor interactor, IScreenPresenter presenter, IScreenController viewController) {
            ScreenId = screenId;
            Interactor = interactor;
            Presenter = presenter;
            ViewController = viewController;
        }
    }

    public interface IScreenModel {
        string ScreenId { get; }
        IScreenInteractor Interactor { get; }
        IScreenPresenter Presenter { get; }
        IScreenController ViewController { get; }
    }
}