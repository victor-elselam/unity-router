using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Installers;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sample.UsageWithoutDependencyInjection.Screens.ScreenB.Presenter
{
    public class ScreenB : MonoBehaviour, IScreenPresenter
    {
        private const string SliderPosKey = "sliderPos";
        [SerializeField] private Button loadScene;
        [SerializeField] private Slider slider;

        public Transform Transform => transform;

        public void Start()
        {
            loadScene.onClick.AddListener(() => UnityRouter.Navigation.NavigateTo(new ScreenScheme("", "OtherScene")));
        }

        public void OnEnter([CanBeNull] IDictionary<string, string> parameters)
        {
            slider.SetValueWithoutNotify(UnityRouter.Parameters.GetParamOfType<float>(parameters, SliderPosKey));
        }

        public IDictionary<string, string> OnExit()
        {
            var sliderPos = UnityRouter.Parameters.Create(SliderPosKey, slider.value);
            return UnityRouter.Parameters.CreateDictionary(sliderPos);
        }

        public void Enable() => Transform.gameObject.SetActive(true);
        public void Disable() => Transform.gameObject.SetActive(false);
    }
}
