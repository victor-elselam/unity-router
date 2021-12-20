using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.Installers;
using Sample.ChangeSceneSample.Screens.ScreenB.Presenter;
using System.Collections.Generic;

namespace Sample.ChangeSceneSample.Screens.ScreenB.Interactor
{
    public class ScreenBInteractor : BaseScreenInteractor, IScreenBInteractor
    {
        private float elementPosition = 0;
        private readonly IScreenBPresenter presenter;
        private readonly INavigation navigation;

        public ScreenBInteractor(IScreenBPresenter presenter, INavigation navigation)
        {
            this.presenter = presenter;
            this.navigation = navigation;
        }

        public override void WithParameters(IDictionary<string, string> parameters)
        {
            if (parameters.TryGetValue("elementPosition", out var position))
            {
                elementPosition = float.Parse(position);
                presenter.SliderPosition(elementPosition);
            }
        }

        public void LoadScene(string scene)
        {
            navigation.NavigateTo(scene);
        }

        public void UpdateElementPosition(float position)
        {
            elementPosition = position;
        }

        public override IDictionary<string, string> OnExit()
        {
            var parameters = new Dictionary<string, string>();
            parameters["elementPosition"] = $"{elementPosition}";
            return parameters;
        }
    }
}
