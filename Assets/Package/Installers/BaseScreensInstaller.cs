using System.Collections.Generic;
using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    public abstract class BaseScreensInstaller : ScriptableObject
    {
        public abstract List<IScreenRegistry> GetScreens();
    }
}
