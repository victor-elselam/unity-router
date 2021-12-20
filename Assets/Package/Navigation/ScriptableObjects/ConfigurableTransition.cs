
using Cysharp.Threading.Tasks;
using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.Transitions;
using JetBrains.Annotations;
using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    [CreateAssetMenu(fileName = "New Transition", menuName = "Elselam/UnityRouter")]
    public abstract class ConfigurableTransition : ScriptableObject, ITransition
    {
        public abstract UniTask Transite([CanBeNull] IScreenPresenter enterScreen, [CanBeNull] IScreenPresenter exitScreen);
    }
}