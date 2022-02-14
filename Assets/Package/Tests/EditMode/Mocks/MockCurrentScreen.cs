using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Installers;

namespace Elselam.UnityRouter.Tests.Mocks
{
    public class MockCurrentScreen : ICurrentScreen
    {
        private ScreenScheme scheme;

        public ScreenScheme Scheme => scheme;

        public void SetCurrentScreen(ScreenScheme scheme)
        {
            this.scheme = scheme;
        }
    }
}