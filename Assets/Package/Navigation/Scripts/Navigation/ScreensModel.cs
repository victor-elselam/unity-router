using Elselam.UnityRouter.Domain;

namespace Elselam.UnityRouter.Installers
{

    public class ScreenModel : IScreenModel
    {
        public string ScreenId { get; }
        public IScreenInteractor Interactor { get; }
        public IScreenPresenter Presenter { get; }
        public IScreenView View { get; }

        public ScreenModel(string screenId, IScreenInteractor interactor, IScreenPresenter presenter, IScreenView view)
        {
            ScreenId = screenId;
            Interactor = interactor;
            Presenter = presenter;
            View = view;
        }
    }
}