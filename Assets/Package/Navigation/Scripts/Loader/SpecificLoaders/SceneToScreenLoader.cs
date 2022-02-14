using Cysharp.Threading.Tasks;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.SceneLoad;
using Elselam.UnityRouter.ScreenLoad;
using Elselam.UnityRouter.Transitions;

namespace Assets.Package.Navigation.Scripts.Loader.SpecificLoaders
{
    public class SceneToScreenLoader : ISpecificLoader
    {
        private IScreenLoader screenLoader;
        private ISceneLoader sceneLoader;

        public SceneToScreenLoader(IScreenLoader screenLoader, ISceneLoader sceneLoader)
        {
            this.screenLoader = screenLoader;
            this.sceneLoader = sceneLoader;
        }

        public async UniTask<ScreenScheme> Load(ScreenScheme enterScheme, ScreenScheme exitScheme = null, ITransition transition = null, bool back = false)
        {
            await sceneLoader.LoadMainScene();
            await UniTask.Delay(100); //we need this to give time for Start() to happen and screens get initialized
            await screenLoader.LoadScreen(enterScheme);
            await screenLoader.Transition(enterScheme, null, transition);

            return exitScheme;
        }
    }
}
