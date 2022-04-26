using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.Installers;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sample.UsageWithoutDependencyInjection.Screens.ScreenA.Presenter
{
    public class ScreenA : MonoBehaviour, IScreenPresenter
    {
        [SerializeField] private Button nextScreenButton;

        public Transform Transform => transform;

        public void Enable() => Transform.gameObject.SetActive(true);
        public void Disable() => Transform.gameObject.SetActive(false);
        public void OnEnter([CanBeNull] IDictionary<string, string> parameters) { }
        public IDictionary<string, string> OnExit() { return null; }

        public void Start()
        {
            nextScreenButton.onClick.AddListener(() => UnityRouter.Navigation.NavigateTo<ScreenB.Presenter.ScreenB>());
        }
    }
}
