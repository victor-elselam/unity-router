using Cysharp.Threading.Tasks;
using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Installers;
using Elselam.UnityRouter.Transitions;
using Elselam.UnityRouter.Url;

namespace Elselam.UnityRouter.ScreenLoad
{
    public class ScreenLoader : IScreenLoader
    {
        private IUrlManager urlManager;
        private IScreenResolver screenResolver;
        private ITransition defaultTransition;

        public ScreenLoader(IUrlManager urlManager, IScreenResolver screenResolver, ITransition defaultTransition)
        {
            this.urlManager = urlManager;
            this.screenResolver = screenResolver;
            this.defaultTransition = defaultTransition;
        }

        public async UniTask LoadScreen(ScreenScheme enterScheme)
        {
            var enterScreenModel = screenResolver.GetScreenModel(enterScheme.ScreenId);
            if (enterScreenModel == null)
                throw new NavigationException($"Invalid target screen: {enterScheme.ScreenId}");

            enterScreenModel.Interactor.OnEnter(enterScheme.Parameters);
        }

        public async UniTask Transition(ScreenScheme enterScheme, ScreenScheme exitScheme = null, ITransition transition = null)
        {
            var enterId = enterScheme?.ScreenId ?? "";
            var exitId = exitScheme?.ScreenId ?? "";
            if (enterId == exitId)
                return;

            transition ??= defaultTransition;
            var enter = screenResolver.GetScreenModel(enterId)?.Presenter;
            var exit = screenResolver.GetScreenModel(exitId)?.Presenter;
            await transition.Transite(enter, exit);
        }

        public ScreenScheme UnloadScreen(ScreenScheme exitScheme, bool back = false)
        {
            if (exitScheme == null)
                return null;

            var exitScreenModel = screenResolver.GetScreenModel(exitScheme.ScreenId);
            var parameters = exitScreenModel.Interactor.OnExit();

            if (!back)
            {
                var url = urlManager.BuildToString(exitScreenModel.ScreenId, parameters);
                return urlManager.Deserialize(url);
            }

            return null;
        }
    }
}