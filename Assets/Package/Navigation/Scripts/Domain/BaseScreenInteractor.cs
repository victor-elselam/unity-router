using System.Collections.Generic;

namespace Elselam.UnityRouter.Domain
{
    public abstract class BaseScreenInteractor : IScreenInteractor
    {
        public virtual void OnEnter(IDictionary<string, string> parameters)
        {
        }

        public virtual IDictionary<string, string> OnExit()
        {
            return null;
        }
    }
}