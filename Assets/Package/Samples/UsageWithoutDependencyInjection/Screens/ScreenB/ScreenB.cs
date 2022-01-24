using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Installers;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sample.UsageWithoutDependencyInjection.Screens.ScreenB.Presenter
{
    public class ScreenB : BaseScreen
    {
        private const string SliderPosKey = "sliderPos";
        [SerializeField] private Button loadScene;
        [SerializeField] private Slider slider;

        public void Start()
        {
            loadScene.onClick.AddListener(() => UnityRouter.Navigation.NavigateTo(new ScreenScheme("", "OtherScene")));
        }

        public override void OnEnter([CanBeNull] IDictionary<string, string> parameters)
        {
            slider.SetValueWithoutNotify(UnityRouter.Parameters.GetParamOfType<float>(parameters, SliderPosKey));
        }

        public override IDictionary<string, string> OnExit()
        {
            var sliderPos = UnityRouter.Parameters.Create<float>(SliderPosKey, slider.value);
            return UnityRouter.Parameters.CreateDictionary(sliderPos);
        }
    }
}
