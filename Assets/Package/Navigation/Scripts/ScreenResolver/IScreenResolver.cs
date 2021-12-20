using Elselam.UnityRouter.History;

namespace Elselam.UnityRouter.Installers
{
    public interface IScreenResolver
    {
        /// <summary>
        /// Resolve deep link received
        /// </summary>
        /// <returns></returns>
        ScreenScheme ResolveScheme();

        /// <summary>
        /// TODO: Remove this from interface and mock the event
        /// </summary>
        /// <param name="deepLinkUrl"></param>
        void ApplicationOnDeepLinkActivated(string deepLinkUrl);

        /// <summary>
        /// Start to listen to deep links. Activate immediately if we already have a deep link in system cache
        /// </summary>
        void Initialize();
    }
}