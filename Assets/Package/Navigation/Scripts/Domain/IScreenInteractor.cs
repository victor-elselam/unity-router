using System.Collections.Generic;

namespace Elselam.UnityRouter.Domain
{
    public interface IScreenInteractor
    {
        void OnEnter();
        IDictionary<string, string> OnExit();
        void WithParameters(IDictionary<string, string> parameters);
    }
}