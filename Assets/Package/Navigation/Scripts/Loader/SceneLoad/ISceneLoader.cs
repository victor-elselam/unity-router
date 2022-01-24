using Assets.Package.Navigation.Scripts.Loader;
using Cysharp.Threading.Tasks;

namespace Elselam.UnityRouter.SceneLoad
{
    public interface ISceneLoader : IPresentationLoader
    {
        /// <summary>
        /// Load the Main Scene, specified in 'Navigation Installer', in field 'mainSceneName'
        /// </summary>
        /// <returns></returns>
        UniTask LoadMainScene();
    }
}