using Elselam.UnityRouter.Installers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sample.UsageWithoutDependencyInjection.Scripts
{
    public class ChangeSceneAppStart : MonoBehaviour
    {
        [SerializeField] private NavigationSettings settings;
        [SerializeField] private List<ScreenRegistryObject> screenList;
        [SerializeField] private Transform screensContainer;

        private static bool firstLoad;

        private void Start()
        {
            var list = screenList.Select(sl => sl.ScreenRegistry).Cast<IScreenRegistry>().ToList();

            UnityRouter.Setup(settings, list, screensContainer);
            UnityRouter.Create();

            if (!firstLoad)
            {
                UnityRouter.Navigation.NavigateToDefaultScreen();
                firstLoad = true;
            }
        }
    }
}