
using UnityEngine;

namespace elselam.Navigation.ScriptableObjects {
    [CreateAssetMenu(fileName = "New Screen", menuName = "Elselam/UNavScreen/NewScreen")]
    public class ScreenRegistryObject : ScriptableObject {
        public ScreenRegistry ScreenRegistry;
    }
}