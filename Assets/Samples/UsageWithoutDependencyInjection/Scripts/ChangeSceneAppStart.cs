using Elselam.UnityRouter.Installers;
using Sample.UsageWithoutDependencyInjection.Screens.ScreenA.Presenter;
using Sample.UsageWithoutDependencyInjection.Screens.ScreenB.Presenter;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sample.UsageWithoutDependencyInjection.Scripts
{
    public class ChangeSceneAppStart : MonoBehaviour
    {
        [SerializeField] private NavigationSettings settings;
        [SerializeField] private Transform screensContainer;

        [SerializeField] private GameObject screenAPrefab;
        [SerializeField] private GameObject screenBPrefab;

        private static bool firstLoad = true;

        private void Start()
        {
            if (firstLoad)
            {
                UnityRouter.Setup(settings, GetScreens());
                UnityRouter.Create();

                UnityRouter.Navigation.NavigateToDefaultScreen();
                firstLoad = false;
            }
        }

        private List<IScreenRegistry> GetScreens()
        {
            var list = new List<IScreenRegistry>();
            list.Add(new ScreenRegistry("ScreenA", typeof(ScreenA), screenAPrefab));
            list.Add(new ScreenRegistry("ScreenB", typeof(ScreenB), screenBPrefab));

            return list;
        }
    }
}