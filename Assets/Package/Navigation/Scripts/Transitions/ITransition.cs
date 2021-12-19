using Cysharp.Threading.Tasks;
using Elselam.UnityRouter.Domain;
using JetBrains.Annotations;

namespace Elselam.UnityRouter.Transitions
{
    public interface ITransition
    {
        /// <summary>
        /// Perform transition
        /// </summary>
        /// <param name="enterScreen">Screen entering</param>
        /// <param name="exitScreen">Screen exiting</param>
        /// <returns>Awaitable task, finishs when transition end</returns>
        UniTask Transite([CanBeNull] IScreenPresenter enterScreen, [CanBeNull] IScreenPresenter exitScreen);
    }
}