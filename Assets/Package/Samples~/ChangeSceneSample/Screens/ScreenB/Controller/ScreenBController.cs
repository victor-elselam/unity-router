using Elselam.UnityRouter.Domain;
using Sample.ChangeSceneSample.Screens.ScreenB.Interactor;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sample.ChangeSceneSample.Screens.ScreenB.Controller
{
    public class ScreenBController : BaseScreenView, IScreenBController
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Button loadScene;

        [Inject]
        public void Construct(IScreenBInteractor interactor)
        {
            slider.OnValueChangedAsObservable().Subscribe(interactor.UpdateElementPosition);
            loadScene.onClick.AddListener(() => interactor.LoadScene("SceneWithVideoPlayer"));
        }
    }
}
