using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.Extensions;
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
        private readonly IParameterManager parameterManager;

        public ScreenBInteractor(IScreenBPresenter presenter, INavigation navigation, IParameterManager parameterManager)
        {
            this.presenter = presenter;
            this.navigation = navigation;
            this.parameterManager = parameterManager;
        }

        public override void OnEnter(IDictionary<string, string> parameters)
        {
            elementPosition = parameterManager.GetParamOfType<float>(parameters, "elementPosition", defaultValue: elementPosition);
            presenter.SliderPosition(elementPosition);
        }

        public void BackToLastScreen()
        {
            navigation.BackToLastScreen();
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
            var paramPosition = parameterManager.Create("elementPosition", elementPosition);
            var parameters = parameterManager.CreateDictionary(paramPosition);
            return parameters;
        }
    }
}
