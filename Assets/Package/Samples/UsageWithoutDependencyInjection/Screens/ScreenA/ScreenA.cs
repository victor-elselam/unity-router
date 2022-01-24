using Elselam.UnityRouter.Domain;
using Sample.UsageWithoutDependencyInjection.Screens.ScreenB.Presenter;
using UnityEngine;
using UnityEngine.UI;

namespace Sample.UsageWithoutDependencyInjection.Screens.ScreenA.Presenter
{
    public class ScreenA : BaseScreen
    {
        [SerializeField] private Button nextScreenButton;

        public void Start()
        {
            nextScreenButton.onClick.AddListener(() => Elselam.UnityRouter.Installers.UnityRouter.Navigation.NavigateTo<ScreenB.Presenter.ScreenB>());
        }
    }
}
