using Assets.Package.Navigation.Scripts.Loader.SpecificLoaders;
using Elselam.UnityRouter.Extensions;
using Elselam.UnityRouter.SceneLoad;
using Elselam.UnityRouter.ScreenLoad;
using UnityEngine;

namespace Assets.Package.Navigation.Scripts.Loader
{
    public class LoaderFactory : ILoaderFactory
    {
        private ISceneLoader sceneLoader;
        private IScreenLoader screenLoader;

        private ISpecificLoader sceneToScene;
        private ISpecificLoader sceneToScreen;
        private ISpecificLoader screenToScene;
        private ISpecificLoader screenToScreen;

        public LoaderFactory(ISceneLoader sceneLoader, IScreenLoader screenLoader)
        {
            this.sceneLoader = sceneLoader;
            this.screenLoader = screenLoader;

            sceneToScene = new SceneToSceneLoader(sceneLoader);
            sceneToScreen = new SceneToScreenLoader(screenLoader, sceneLoader);
            screenToScene = new ScreenToSceneLoader(screenLoader, sceneLoader);
            screenToScreen = new ScreenToScreenLoader(screenLoader);
        }

        public bool CanSceneBeLoaded(string sceneName) => Application.CanStreamedLevelBeLoaded(sceneName);

        public ISpecificLoader GetLoader(string targetId, string exitId)
        {
            if (exitId.IsNullOrEmpty())
            {
                if (CanSceneBeLoaded(targetId))
                    return sceneToScene;

                return screenToScreen;
            }
            else
            {
                if (CanSceneBeLoaded(targetId) && CanSceneBeLoaded(exitId))
                    return sceneToScene;
                else if (CanSceneBeLoaded(targetId) && !CanSceneBeLoaded(exitId))
                    return screenToScene;
                else if (!CanSceneBeLoaded(targetId) && CanSceneBeLoaded(exitId))
                    return sceneToScreen;
                else
                    return screenToScreen;
            }
        }
    }
}
