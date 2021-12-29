using Elselam.UnityRouter.History;
using Elselam.UnityRouter.SceneLoad;
using Elselam.UnityRouter.ScreenLoad;
using Elselam.UnityRouter.Transitions;

namespace Assets.Package.Navigation.Scripts.Loader.SpecificLoaders
{
    public class ScreenToSceneLoader : ILoader
    {
        private IScreenLoader screenLoader;
        private ISceneLoader sceneLoader;

        public ScreenToSceneLoader(IScreenLoader screenLoader, ISceneLoader sceneLoader)
        {
            this.screenLoader = screenLoader;
            this.sceneLoader = sceneLoader;
        }

        public ScreenScheme Load(ScreenScheme enterScheme, ScreenScheme exitSheme = null, ITransition transition = null, bool back = false)
        {
            var returnScheme = screenLoader.UnloadScreen(exitSheme, back);
            screenLoader.Transition(null, exitSheme, transition);
            sceneLoader.LoadScreen(enterScheme);

            return returnScheme;
        }
    }
}
