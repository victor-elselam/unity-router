using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using Zenject;

namespace Elselam.UnityRouter.SceneLoad
{
    public class DISceneLoader : ISceneLoader
    {
        private readonly string loadingSceneName;
        private readonly string mainSceneName;
        private readonly ZenjectSceneLoader sceneLoader;

        [Inject]
        public DISceneLoader(string loadingSceneName, string mainSceneName, ZenjectSceneLoader sceneLoader)
        {
            this.loadingSceneName = loadingSceneName;
            this.mainSceneName = mainSceneName;
            this.sceneLoader = sceneLoader;
        }

        public async UniTask LoadLoadingScene() =>
            await sceneLoader.LoadSceneAsync(loadingSceneName, LoadSceneMode.Additive);
        public async UniTask UnloadLoadingScene() =>
            await SceneManager.UnloadSceneAsync(loadingSceneName);
        public async UniTask LoadScene(string sceneName, Action<DiContainer> extraBindings = null) =>
            await sceneLoader.LoadSceneAsync(sceneName, LoadSceneMode.Single, extraBindings);
        public async UniTask LoadMainScene(Action<DiContainer> extraBindings = null) =>
            await sceneLoader.LoadSceneAsync(mainSceneName, LoadSceneMode.Single, extraBindings);
    }
}