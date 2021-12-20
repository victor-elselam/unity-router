using Cysharp.Threading.Tasks;
using Elselam.UnityRouter.Domain;
using JetBrains.Annotations;

namespace Elselam.UnityRouter.Transitions
{
    public interface ITransition
    {
        UniTask Transite([CanBeNull] IScreenPresenter enterScreen, [CanBeNull] IScreenPresenter exitScreen);
    }

}