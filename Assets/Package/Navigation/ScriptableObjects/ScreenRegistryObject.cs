using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    [CreateAssetMenu(fileName = "New Screen", menuName = "Elselam/UnityRouter/CreateScreen")]
    public class ScreenRegistryObject : ScriptableObject
    {
        public ScreenRegistry ScreenRegistry;
    }
}