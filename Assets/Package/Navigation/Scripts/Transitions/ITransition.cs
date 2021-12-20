using Cysharp.Threading.Tasks;
using Elselam.UnityRouter.Domain;
using JetBrains.Annotations;

namespace Elselam.UnityRouter.Transitions
{
    public interface ITransition
    {
        /// <summary>
        /// Perform transition defined in the method. 
        /// You have all the freedom to create your own transitions, as long as you return a UniTask to allow the framework to wait for it
        /// </summary>
        /// <param name="enterScreen">Screen entering</param>
        /// <param name="exitScreen">Screen exiting</param>
        /// <returns>Awaitable task, finishs when transition end</returns>
        UniTask Transite([CanBeNull] IScreenPresenter enterScreen, [CanBeNull] IScreenPresenter exitScreen);
    }
}