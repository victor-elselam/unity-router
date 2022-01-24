using Cysharp.Threading.Tasks;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Transitions;
using System;
using UnityEngine.SceneManagement;
using Zenject;

namespace Elselam.UnityRouter.SceneLoad
{
    public class DefaultSceneLoader : ISceneLoader
    {
        private readonly string loadingSceneName;
        private readonly string mainSceneName;

        public DefaultSceneLoader(string loadingSceneName, string mainSceneName)
        {
            this.loadingSceneName = loadingSceneName;
            this.mainSceneName = mainSceneName;
        }

        public async UniTask LoadLoadingScene() =>
           await SceneManager.LoadSceneAsync(loadingSceneName, LoadSceneMode.Additive);
        public async UniTask UnloadLoadingScene() =>
            await SceneManager.UnloadSceneAsync(loadingSceneName);
        public async UniTask LoadScene(string sceneName, Action<DiContainer> extraBindings = null) =>
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        public async UniTask LoadMainScene()
        {
            await LoadLoadingScene();
            await LoadScene(mainSceneName);
        }

        public async UniTask LoadScreen(ScreenScheme enterScheme)
        {
            await LoadLoadingScene();
            await LoadScene(enterScheme.ScreenId);
        }

        public UniTask Transition(ScreenScheme enterScheme, ScreenScheme exitScheme, ITransition transition = null)
        {
            throw new NotImplementedException();
        }

        public ScreenScheme UnloadScreen(ScreenScheme exitScheme, bool back = false) => exitScheme;
    }
}