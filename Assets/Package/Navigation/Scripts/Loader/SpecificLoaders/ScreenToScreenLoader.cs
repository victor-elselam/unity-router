using Cysharp.Threading.Tasks;
using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.ScreenLoad;
using Elselam.UnityRouter.Transitions;

namespace Assets.Package.Navigation.Scripts.Loader.SpecificLoaders
{
    public class ScreenToScreenLoader : ISpecificLoader
    {
        private IScreenLoader screenLoader;

        public ScreenToScreenLoader (IScreenLoader screenLoader)
        {
            this.screenLoader = screenLoader;
        }

        public async UniTask<ScreenScheme> Load(ScreenScheme enterScheme, ScreenScheme exitScheme = null, ITransition transition = null, bool back = false)
        {
            var returnScheme = screenLoader.UnloadScreen(exitScheme, back);
            screenLoader.Transition(enterScheme, exitScheme, transition);
            screenLoader.LoadScreen(enterScheme);

            return returnScheme;
        }
    }
}
