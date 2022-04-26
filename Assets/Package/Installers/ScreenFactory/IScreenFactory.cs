
namespace Elselam.UnityRouter.Installers
{
    public interface IScreenFactory
    {
        IScreenModel Create(IScreenRegistry screenRegistry);
    }
}