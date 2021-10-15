using Cysharp.Threading.Tasks;
using elselam.Navigation.Domain;

namespace elselam.Navigation.Transitions {
    public class DefaultTransition : ITransition {
        public async UniTask Transite(IScreenPresenter enterScreen, IScreenPresenter exitScreen) {
            if (enterScreen == exitScreen)
                return;
            
            if (exitScreen != null)
                exitScreen.Disable();
            if (enterScreen != null)
                enterScreen.Enable();
        }
    }
}