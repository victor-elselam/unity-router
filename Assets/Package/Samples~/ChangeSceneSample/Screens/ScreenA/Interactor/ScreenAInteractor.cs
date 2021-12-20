using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.Installers;
using Sample.ChangeSceneSample.Screens.ScreenB.Interactor;

namespace Sample.ChangeSceneSample.Screens.ScreenA.Interactor
{
    public class ScreenAInteractor : BaseScreenInteractor, IScreenAInteractor
    {
        private readonly INavigation navigation;

        public ScreenAInteractor(INavigation navigation)
        {
            this.navigation = navigation;
        }

        public void LoadScreenB()
        {
            navigation.NavigateTo<ScreenBInteractor>();
        }
    }
}
