using UnityEngine;

namespace elselam.Navigation.Domain {
    public abstract class BaseCanvasScreenPresenter : BaseScreenPresenter {
        [SerializeField] private Transform container;
        public override Transform Transform => container;
    }
}