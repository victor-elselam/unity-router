
using elselam.Navigation.History;

namespace elselam.Navigation.Navigation {
    public interface IScreenResolver {
        ScreenScheme ResolveScheme();
        void ApplicationOnDeepLinkActivated(string deepLinkUrl);
        void Initialize();
    }
}