using UnityEngine;

namespace Elselam.UnityRouter.Domain
{
    public interface IScreenPresenter
    {
        Transform Transform { get; }
        void Enable();
        void Disable();
    }
}