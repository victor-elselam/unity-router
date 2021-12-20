using Elselam.UnityRouter.Domain;

namespace Elselam.UnityRouter.Installers
{
    public interface IScreenModel
    {
        /// <summary>
        /// Screen Name, also is the Screen Id
        /// </summary>
        string ScreenId { get; }

        /// <summary>
        /// Screen Interactor
        /// </summary>
        IScreenInteractor Interactor { get; }

        /// <summary>
        /// Screen Presenter
        /// </summary>
        IScreenPresenter Presenter { get; }

        /// <summary>
        /// Screen View
        /// </summary>
        IScreenView View { get; }
    }
}