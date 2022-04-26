using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Elselam.UnityRouter.Installers
{
    [CreateAssetMenu(fileName = "New Screens Installer", menuName = "Elselam/UnityRouter/Installers/Area", order = 0)]
    public class AreaInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private BaseScreensInstaller[] screensInstaller;

        public override void InstallBindings()
        {
            var registries = screensInstaller.SelectMany(si => si.GetScreens()).ToList();

            foreach (var screenRegistry in registries)
            {
                Container.Bind(screenRegistry.ScreenPresenter)
                    .WithId(screenRegistry.ScreenId)
                    .AsSingle();
            }

            Container.Bind<List<IScreenRegistry>>()
                .FromInstance(registries)
                .AsSingle();
        }
    }
}