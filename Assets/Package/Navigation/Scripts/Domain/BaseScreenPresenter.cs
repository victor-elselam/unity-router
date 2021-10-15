using UnityEngine;

namespace elselam.Navigation.Domain {
    public abstract class BaseScreenPresenter : MonoBehaviour, IScreenPresenter {
        public virtual Transform Transform => transform;
        
        public virtual void Enable() {
            gameObject.SetActive(true);
        }

        public virtual void Disable() {
            gameObject.SetActive(false);
        }
    }
}