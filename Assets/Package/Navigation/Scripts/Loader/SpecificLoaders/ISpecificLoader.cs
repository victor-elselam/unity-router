using Cysharp.Threading.Tasks;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Transitions;

namespace Assets.Package.Navigation.Scripts.Loader.SpecificLoaders
{
    public interface ISpecificLoader
    {
        UniTask<ScreenScheme> Load(ScreenScheme enterScheme, ScreenScheme exitScheme = null, ITransition transition = null, bool back = false);
    }
}
