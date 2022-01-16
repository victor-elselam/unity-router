using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Transitions;

namespace Assets.Package.Navigation.Scripts.Loader.SpecificLoaders
{
    public interface ISpecificLoader
    {
        ScreenScheme Load(ScreenScheme enterScheme, ScreenScheme exitSheme = null, ITransition transition = null, bool back = false);
    }
}
