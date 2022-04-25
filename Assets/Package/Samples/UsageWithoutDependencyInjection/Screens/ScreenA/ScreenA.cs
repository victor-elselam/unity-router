using Elselam.UnityRouter.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace Sample.UsageWithoutDependencyInjection.Screens.ScreenA.Presenter
{
    public class ScreenA : BaseScreenPresenter
    {
        [SerializeField] private Button nextScreenButton;

        public void Start()
        {
            nextScreenButton.onClick.AddListener(() => Elselam.UnityRouter.Installers.UnityRouter.Navigation.NavigateTo<ScreenB.Presenter.ScreenB>());
        }
    }
}
