using Cysharp.Threading.Tasks;
using elselam.Navigation.Domain;
using UnityEngine;

namespace elselam.Navigation.Transitions {
    public class ToLeftTransition : ITransition {
        public async UniTask Transite(IScreenPresenter enterScreen, IScreenPresenter exitScreen) {
            enterScreen.Enable();
            enterScreen.Transform.localPosition = new Vector3(-Screen.width, 0, 0);
            await Move(enterScreen.Transform, Vector3.zero);
            exitScreen?.Disable();
        }

        private async UniTask Move(Transform target, Vector3 position) {
            while (target.localPosition.x < position.x) {
                target.localPosition = new Vector3(target.localPosition.x + 10, target.localPosition.y, target.localPosition.z);
                await UniTask.NextFrame();
            }
        }
    }
}