
using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.History;

namespace Elselam.UnityRouter.Installers
{
    public interface ICurrentScreen
    {
        IScreenInteractor Screen { get; }
        ScreenScheme Scheme { get; }
        void SetCurrentScreen(IScreenInteractor screenController, ScreenScheme scheme);
    }
}