
using Elselam.UnityRouter.Domain;

namespace Elselam.UnityRouter.Installers
{

    public class ScreenModel : IScreenModel
    {
        public string ScreenId { get; }
        public IScreenInteractor Interactor { get; }
        public IScreenPresenter Presenter { get; }
        public IScreenView ViewController { get; }

        public ScreenModel(string screenId, IScreenInteractor interactor, IScreenPresenter presenter, IScreenView viewController)
        {
            ScreenId = screenId;
            Interactor = interactor;
            Presenter = presenter;
            ViewController = viewController;
        }
    }
}