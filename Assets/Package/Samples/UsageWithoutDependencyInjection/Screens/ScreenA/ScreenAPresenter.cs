using Elselam.UnityRouter.Domain;
using Sample.UsageWithoutDependencyInjection.Screens.ScreenB.Presenter;
using UnityEngine;
using UnityEngine.UI;

namespace Sample.UsageWithoutDependencyInjection.Screens.ScreenA.Presenter
{
    public class ScreenAPresenter : BaseScreen
    {
        [SerializeField] private Button nextScreenButton;

        public void Start()
        {
            nextScreenButton.onClick.AddListener(() => Elselam.UnityRouter.Installers.UnityRouter.Navigation.NavigateTo<ScreenBPresenter>());
        }
    }
}
