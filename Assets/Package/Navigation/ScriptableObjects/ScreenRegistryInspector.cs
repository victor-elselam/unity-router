using System;
using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    [CreateAssetMenu(fileName = "ScreenRegistry", menuName = "Elselam/UnityRouter/New Screen")]
    public class ScreenRegistryInspector : ScriptableObject, IScreenRegistry
    {
        [SerializeField] private ScreenRegistry screenRegistry;

        public string ScreenId => screenRegistry.ScreenId;
        public Type ScreenPresenter => screenRegistry.ScreenPresenter;
        public GameObject ScreenPrefab => screenRegistry.ScreenPrefab;
    }
}