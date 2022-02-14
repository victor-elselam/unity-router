using Elselam.UnityRouter.Domain;
using Sample.ChangeSceneSample.Screens.ScreenA.Interactor;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sample.ChangeSceneSample.Screens.ScreenA.Controller
{
    public class ScreenAView : BaseScreenView, IScreenAView
    {
        [SerializeField] private Button loadNextScreen;

        [Inject]
        public void Construct(IScreenAInteractor interactor)
        {
            loadNextScreen.onClick.AddListener(() => interactor.LoadScreenB());
        }
    }
}
