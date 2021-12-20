using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.History;

namespace Elselam.UnityRouter.Installers
{
    public interface ICurrentScreen
    {
        /// <summary>
        /// Interactor of the current screen
        /// </summary>
        IScreenInteractor Screen { get; }

        /// <summary>
        /// Current screen metadata
        /// </summary>
        ScreenScheme Scheme { get; }

        /// <summary>
        /// Set current screen. Be careful using this.
        /// </summary>
        /// <param name="screenController"></param>
        /// <param name="scheme"></param>
        void SetCurrentScreen(IScreenInteractor screenController, ScreenScheme scheme);
    }
}