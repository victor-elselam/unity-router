using System.Collections.Generic;

namespace Elselam.UnityRouter.Domain
{
    /// <summary>
    /// This class should be used when implementing UnityRouter WITH Dependency Injection, use it in your interactor class
    /// </summary>
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