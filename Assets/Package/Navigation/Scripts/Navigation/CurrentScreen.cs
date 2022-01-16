using Elselam.UnityRouter.History;

namespace Elselam.UnityRouter.Installers
{
    public class CurrentScreen : ICurrentScreen
    {
        public ScreenScheme Scheme { get; private set; }

        public void SetCurrentScreen(ScreenScheme scheme)
        {
            Scheme = scheme;
        }
    }
}