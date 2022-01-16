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

        public ScreenScheme Load(ScreenScheme enterScheme, ScreenScheme exitSheme = null, ITransition transition = null, bool back = false)
        {
            sceneLoader.LoadMainScene();
            screenLoader.LoadScreen(enterScheme);
            screenLoader.Transition(enterScheme, null, transition);

            return exitSheme;
        }
    }
}
