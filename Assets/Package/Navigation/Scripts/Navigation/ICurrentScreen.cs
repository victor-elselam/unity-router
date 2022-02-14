using Elselam.UnityRouter.History;

namespace Elselam.UnityRouter.Installers
{
    public interface ICurrentScreen
    {
        /// <summary>
        /// Current screen metadata
        /// </summary>
        ScreenScheme Scheme { get; }

        /// <summary>
        /// Set current screen. Be careful using this.
        /// </summary>
        /// <param name="screenController"></param>
        /// <param name="scheme"></param>
        void SetCurrentScreen(ScreenScheme scheme);
    }
}