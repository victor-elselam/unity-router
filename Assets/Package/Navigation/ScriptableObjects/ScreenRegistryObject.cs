using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    [CreateAssetMenu(fileName = "New Screen", menuName = "Elselam/UnityRouter")]
    public class ScreenRegistryObject : ScriptableObject
    {
        public ScreenRegistry ScreenRegistry;
    }
}