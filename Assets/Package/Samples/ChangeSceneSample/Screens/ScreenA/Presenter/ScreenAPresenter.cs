using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.Installers;
using JetBrains.Annotations;
using Sample.ChangeSceneSample.Screens.ScreenB.Presenter;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sample.ChangeSceneSample.Screens.ScreenA.Presenter
{
    public class ScreenAPresenter : MonoBehaviour, IScreenPresenter
    {
        [SerializeField] private Button loadScreenB;
        public Transform Transform => transform;

        [Inject]
        public void Inject(INavigation navigation)
        {
            loadScreenB.onClick.AddListener(() => navigation.NavigateTo<ScreenBPresenter>());
        }

        public void Enable() => Transform.gameObject.SetActive(true);
        public void Disable() => Transform.gameObject.SetActive(false);

        public void OnEnter([CanBeNull] IDictionary<string, string> parameters) { }
        public IDictionary<string, string> OnExit() { return null; }
    }
}
