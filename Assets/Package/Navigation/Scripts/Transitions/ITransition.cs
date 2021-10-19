using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using elselam.Navigation.Domain;

namespace elselam.Navigation.Transitions {
    public interface ITransition {
        UniTask Transite([CanBeNull] IScreenPresenter enterScreen, [CanBeNull] IScreenPresenter exitScreen);
    }
}