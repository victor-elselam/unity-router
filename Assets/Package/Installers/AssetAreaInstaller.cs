using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    [CreateAssetMenu(fileName = "New Area Installer", menuName = "Elselam/UnityRouter/Installers/Area", order = 0)]
    public class AssetAreaInstaller : BaseAreaInstaller
    {
        [SerializeField] private ScreenRegistryInspector[] screens;

        public override List<IScreenRegistry> GetScreens() => screens.Cast<IScreenRegistry>().ToList();
    }
}
