using Cysharp.Threading.Tasks;
using System;
using Zenject;

namespace Elselam.UnityRouter.SceneLoad
{
    public interface ISceneLoader
    {

        UniTask LoadLoadingScene();
        UniTask UnloadLoadingScene();
        UniTask LoadScene(string sceneName, Action<DiContainer> extraBindings = null);
        UniTask LoadMainScene(Action<DiContainer> extraBindings = null);
    }
}