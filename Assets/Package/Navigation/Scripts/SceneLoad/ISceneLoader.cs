using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace elselam.Navigation.SceneLoad {
    public interface ISceneLoader {

        UniTask LoadLoadingScene();
        UniTask UnloadLoadingScene();
        UniTask LoadScene(string sceneName, Action<DiContainer> extraBindings = null);
        UniTask LoadMainScene(Action<DiContainer> extraBindings = null);
    }
}