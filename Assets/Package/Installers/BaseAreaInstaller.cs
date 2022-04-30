using System.Collections.Generic;
using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    public abstract class BaseAreaInstaller : ScriptableObject
    {
        public abstract List<IScreenRegistry> GetScreens();
    }
}
