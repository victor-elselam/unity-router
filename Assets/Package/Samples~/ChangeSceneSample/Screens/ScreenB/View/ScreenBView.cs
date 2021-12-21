using Elselam.UnityRouter.Domain;
using Sample.ChangeSceneSample.Screens.ScreenB.Interactor;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sample.ChangeSceneSample.Screens.ScreenB.Controller
{
    public class ScreenBView : BaseScreenView, IScreenBView
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Button loadScene;
        [SerializeField] private Button backButton;

        [Inject]
        public void Construct(IScreenBInteractor interactor)
        {
            slider.onValueChanged.AddListener(interactor.UpdateElementPosition);
            loadScene.onClick.AddListener(() => interactor.LoadScene("SceneWithVideoPlayer"));
            backButton.onClick.AddListener(() => interactor.BackToLastScreen());
        }
    }
}
