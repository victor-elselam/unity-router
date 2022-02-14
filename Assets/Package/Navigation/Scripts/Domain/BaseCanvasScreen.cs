using JetBrains.Annotations;
using System.Collections.Generic;

namespace Elselam.UnityRouter.Domain
{
    /// <summary>
    /// This class should be used when implementing UnityRouter WITHOUT Dependency Injection, so your Screen class will have all Control over the screen
    /// </summary>
    public abstract class BaseCanvasScreen : BaseCanvasScreenPresenter, IScreenInteractor
    {
        public virtual void OnEnter([CanBeNull] IDictionary<string, string> parameters)
        {
        }

        public virtual IDictionary<string, string> OnExit() => null;
    }
}