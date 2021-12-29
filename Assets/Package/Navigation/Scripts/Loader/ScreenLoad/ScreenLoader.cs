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

        public ScreenLoader(IUrlManager urlManager, IScreenResolver screenResolver)
        {
            this.urlManager = urlManager;
            this.screenResolver = screenResolver;
            defaultTransition = new DefaultTransition();
        }

        public void LoadScreen(ScreenScheme enterScheme)
        {
            var enterScreenModel = screenResolver.GetScreenModel(enterScheme.ScreenId);
            if (enterScreenModel == null)
                throw new NavigationException($"Invalid target screen: {enterScheme.ScreenId}");

            if (enterScheme.Parameters?.Count > 0)
                enterScreenModel.Interactor.WithParameters(enterScheme.Parameters);

            enterScreenModel.Interactor.OnEnter();
        }

        public async UniTask Transition(ScreenScheme enterScheme, ScreenScheme exitScheme = null, ITransition transition = null)
        {
            transition ??= defaultTransition;
            var enter = screenResolver.GetScreenModel(enterScheme.ScreenId).Presenter;
            var exit = screenResolver.GetScreenModel(exitScheme?.ScreenId)?.Presenter;
            await transition.Transite(enter, exit);
        }

        public ScreenScheme UnloadScreen(ScreenScheme exitScheme, bool back = false)
        {
            var exitScreenModel = screenResolver.GetScreenModel(exitScheme.ScreenId);
            var parameters = exitScreenModel.Interactor.OnExit();
            if (!back)
            {
                var name = screenResolver.GetScreenName(exitScreenModel.Interactor.GetType());
                var url = urlManager.BuildToString(name, parameters);
                return urlManager.Deserialize(url);
            }

            return null;
        }
    }
}