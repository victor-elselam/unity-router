
using Elselam.UnityRouter.Domain;

namespace Elselam.UnityRouter.Installers
{
    public interface IScreenModel
    {
        string ScreenId { get; }
        IScreenInteractor Interactor { get; }
        IScreenPresenter Presenter { get; }
        IScreenView ViewController { get; }
    }
}