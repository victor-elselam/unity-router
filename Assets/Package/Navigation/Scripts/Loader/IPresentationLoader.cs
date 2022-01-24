using Cysharp.Threading.Tasks;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Transitions;

namespace Assets.Package.Navigation.Scripts.Loader
{
    public interface IPresentationLoader
    {
        UniTask LoadScreen(ScreenScheme enterScheme);

        ScreenScheme UnloadScreen(ScreenScheme exitScheme, bool back = false);

        UniTask Transition(ScreenScheme enterScheme, ScreenScheme exitScheme = null, ITransition transition = null);
    }
}
