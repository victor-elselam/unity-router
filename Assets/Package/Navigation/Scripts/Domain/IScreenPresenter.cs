using UnityEngine;

namespace Elselam.UnityRouter.Domain
{
    public interface IScreenPresenter
    {
        /// <summary>
        /// Transform that hold the elements in this screen
        /// </summary>
        Transform Transform { get; }

        /// <summary>
        /// Enable the current screen
        /// </summary>
        void Enable();

        /// <summary>
        /// Disable the current screen
        /// </summary>
        void Disable();
    }
}