using System.Collections.Generic;
using UnityEngine;

namespace Elselam.UnityRouter.Domain
{
    /// <summary>
    /// This class should be used when implementing UnityRouter WITH Dependency Injection, use it in your interactor class
    /// </summary>
    public abstract class BaseScreenPresenter : MonoBehaviour, IScreenPresenter
    {
        public virtual Transform Transform => transform;

        public virtual void Enable()
        {
            gameObject.SetActive(true);
        }

        public virtual void Disable()
        {
            gameObject.SetActive(false);
        }

        public virtual void OnEnter(IDictionary<string, string> parameters)
        {
        }

        public virtual IDictionary<string, string> OnExit()
        {
            return null;
        }
    }
}