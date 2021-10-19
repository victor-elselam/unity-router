using UnityEngine;

namespace elselam.Navigation.Domain {
    public interface IScreenPresenter {
        Transform Transform { get; }
        void Enable();
        void Disable();
    }
}