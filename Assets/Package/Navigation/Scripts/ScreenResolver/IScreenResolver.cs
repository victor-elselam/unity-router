
using Elselam.UnityRouter.History;

namespace Elselam.UnityRouter.Installers
{
    public interface IScreenResolver
    {
        ScreenScheme ResolveScheme();
        void ApplicationOnDeepLinkActivated(string deepLinkUrl);
        void Initialize();
    }
}