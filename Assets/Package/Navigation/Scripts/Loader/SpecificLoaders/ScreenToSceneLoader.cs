using Cysharp.Threading.Tasks;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.SceneLoad;
using Elselam.UnityRouter.ScreenLoad;
using Elselam.UnityRouter.Transitions;

namespace Assets.Package.Navigation.Scripts.Loader.SpecificLoaders
{
    public class ScreenToSceneLoader : ISpecificLoader
    {
        private IScreenLoader screenLoader;
        private ISceneLoader sceneLoader;

        public ScreenToSceneLoader(IScreenLoader screenLoader, ISceneLoader sceneLoader)
        {
            this.screenLoader = screenLoader;
            this.sceneLoader = sceneLoader;
        }

        public async UniTask<ScreenScheme> Load(ScreenScheme enterScheme, ScreenScheme exitScheme = null, ITransition transition = null, bool back = false)
        {
            var returnScheme = screenLoader.UnloadScreen(exitScheme, back);
            screenLoader.Transition(null, exitScheme, transition);
            sceneLoader.LoadScreen(enterScheme);

            return returnScheme;
        }
    }
}
