using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.Installers;
using Sample.ChangeSceneSample.Screens.ScreenB.Presenter;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sample.ChangeSceneSample.Screens.ScreenA.Presenter
{
    public class ScreenAPresenter : BaseCanvasScreenPresenter, IScreenAPresenter
    {
        [SerializeField] private Button loadScreenB;

        [Inject]
        public void Inject(INavigation navigation)
        {
            loadScreenB.onClick.AddListener(() => navigation.NavigateTo<ScreenBPresenter>());
        }
    }
}
