using Elselam.UnityRouter.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace Sample.ChangeSceneSample.Screens.ScreenB.Presenter
{
    public class ScreenBPresenter : BaseCanvasScreenPresenter, IScreenBPresenter
    {
        [SerializeField] private Slider slider;
        public void SliderPosition(float position)
        {
            slider.SetValueWithoutNotify(position);
        }
    }
}
