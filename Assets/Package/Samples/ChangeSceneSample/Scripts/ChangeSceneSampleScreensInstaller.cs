using Elselam.UnityRouter.Installers;
using Sample.ChangeSceneSample.Screens.ScreenA.Presenter;
using Sample.ChangeSceneSample.Screens.ScreenB.Presenter;
using System.Collections.Generic;
using UnityEngine;

namespace Package.Samples.ChangeSceneSample
{
    [CreateAssetMenu(fileName = "New Screens Installer", menuName = "Elselam/UnityRouter/Installers/Area1", order = 0)]
    public class ChangeSceneSampleScreensInstaller : BaseScreensInstaller
    {
        [SerializeField] private GameObject screenAPrefab;
        [SerializeField] private GameObject screenBPrefab;

        public override List<IScreenRegistry> GetScreens()
        {
            var screens = new List<IScreenRegistry>();
            screens.Add(new ScreenRegistry("ScreenA", typeof(ScreenAPresenter), screenAPrefab));
            screens.Add(new ScreenRegistry("ScreenB", typeof(ScreenBPresenter), screenBPrefab));
            return screens;
        }
    }
}
