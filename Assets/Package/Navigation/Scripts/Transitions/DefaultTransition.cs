using Cysharp.Threading.Tasks;
using Elselam.UnityRouter.Domain;

namespace Elselam.UnityRouter.Transitions
{
    public class DefaultTransition : ITransition
    {
        public async UniTask Transite(IScreenPresenter enterScreen, IScreenPresenter exitScreen)
        {
            if (enterScreen == exitScreen)
                return;

            if (exitScreen != null)
                exitScreen.Disable();
            if (enterScreen != null)
                enterScreen.Enable();
        }
    }
}