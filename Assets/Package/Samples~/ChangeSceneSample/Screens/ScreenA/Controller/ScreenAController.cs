using Elselam.UnityRouter.Domain;
using Sample.ChangeSceneSample.Screens.ScreenA.Interactor;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sample.ChangeSceneSample.Screens.ScreenA.Controller
{
    public class ScreenAController : BaseScreenView, IScreenAController
    {
        [SerializeField] private Button loadNextScreen;

        [Inject]
        public void Construct(IScreenAInteractor interactor)
        {
            loadNextScreen.OnClickAsObservable().Subscribe(_ => interactor.LoadScreenB());
        }
    }
}
