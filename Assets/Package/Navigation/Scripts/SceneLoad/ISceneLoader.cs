using Cysharp.Threading.Tasks;
using System;
using Zenject;

namespace Elselam.UnityRouter.SceneLoad
{
    public interface ISceneLoader
    {
        /// <summary>
        /// Load the loading scene. Specified in 'Navigation Installer', in field 'loadingSceneName'
        /// </summary>
        /// <returns></returns>
        UniTask LoadLoadingScene();

        /// <summary>
        /// Unload the loading scene. Specified in 'Navigation Installer', in field 'loadingSceneName'
        /// </summary>
        /// <returns></returns>
        UniTask UnloadLoadingScene();

        /// <summary>
        /// Load the specified scene
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="extraBindings"></param>
        /// <returns></returns>
        UniTask LoadScene(string sceneName, Action<DiContainer> extraBindings = null);

        /// <summary>
        /// Load the Main Scene, specified in 'Navigation Installer', in field 'mainSceneName'
        /// </summary>
        /// <param name="extraBindings"></param>
        /// <returns></returns>
        UniTask LoadMainScene(Action<DiContainer> extraBindings = null);
    }
}