using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.History;

namespace Elselam.UnityRouter.Installers
{
    public class CurrentScreen : ICurrentScreen
    {
        public IScreenInteractor Screen { get; private set; }
        public ScreenScheme Scheme { get; private set; }

        public void SetCurrentScreen(IScreenInteractor screenController, ScreenScheme scheme)
        {
            Screen = screenController;
            Scheme = scheme;
        }
    }
}