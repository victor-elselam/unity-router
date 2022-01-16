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

        public ScreenScheme Load(ScreenScheme enterScheme, ScreenScheme exitSheme = null, ITransition transition = null, bool back = false)
        {
            var returnScheme = screenLoader.UnloadScreen(exitSheme, back);
            screenLoader.Transition(enterScheme, exitSheme, transition);
            screenLoader.LoadScreen(enterScheme);

            return returnScheme;
        }
    }
}
