using Assets.Package.Navigation.Scripts.Loader.SpecificLoaders;

namespace Assets.Package.Navigation.Scripts.Loader
{
    public interface ILoaderFactory
    {
        ILoader GetLoader(string targetId, string exitId);
    }
}
