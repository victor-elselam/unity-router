using Cysharp.Threading.Tasks;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Transitions;
using System;
using System.Collections.Generic;
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
        public async UniTask LoadScene(string sceneName, Action<DiContainer> extraBindings = null) =>
            await sceneLoader.LoadSceneAsync(sceneName, LoadSceneMode.Single, extraBindings);


        public async UniTask UnloadLoadingScene() => await UnloadScene(loadingSceneName);
        public async UniTask UnloadScene(string sceneName) => await SceneManager.UnloadSceneAsync(sceneName);

        public void LoadMainScene()
        {
            Load(mainSceneName);
        }

        public void LoadScreen(ScreenScheme enterScheme)
        {
            Load(enterScheme.ScreenId, container => 
                container.Bind<IDictionary<string, string>>().FromInstance(enterScheme.Parameters));
        }

        public UniTask Transition(ScreenScheme enterScheme, ScreenScheme exitScheme, ITransition transition = null)
        {
            throw new NotImplementedException();
        }

        public ScreenScheme UnloadScreen(ScreenScheme exitScheme, bool back = false)
        {
            Unload(exitScheme.ScreenId);
            return new ScreenScheme("", exitScheme.ScreenId);
        }

        private async void Load(string screenId, Action<DiContainer> extraBindings = null)
        {
            await LoadLoadingScene();
            await LoadScene(screenId, extraBindings);
            await UnloadLoadingScene();
        }

        private async void Unload(string screenId)
        {
            await LoadLoadingScene();
            await UnloadScene(screenId);
            await UnloadLoadingScene();
        }
    }
}