using UnityEngine;

namespace Elselam.UnityRouter.Domain
{
    /// <summary>
    /// This class should be used when implementing UnityRouter WITH Dependency Injection, use it in your presenter class
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
    }
}