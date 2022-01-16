using Elselam.UnityRouter.History;
using Elselam.UnityRouter.SceneLoad;
using Elselam.UnityRouter.Transitions;

namespace Assets.Package.Navigation.Scripts.Loader.SpecificLoaders
{
    public class SceneToSceneLoader : ISpecificLoader
    {
        private ISceneLoader sceneLoader;

        public SceneToSceneLoader(ISceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }

        public ScreenScheme Load(ScreenScheme enterScheme, ScreenScheme exitSheme = null, ITransition transition = null, bool back = false)
        {
            sceneLoader.LoadScreen(enterScheme);

            return exitSheme;
        }
    }
}
