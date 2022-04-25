using JetBrains.Annotations;
using System.Collections.Generic;
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

        /// <summary>
        /// Notify Screen Interactor that this screen is entering.
        /// Inject parameters from last screen or deep link, if any.
        /// </summary>
        /// <param name="parameters">Parameters sent from last screen or deep link. Can be null!</param>
        void OnEnter([CanBeNull] IDictionary<string, string> parameters);

        /// <summary>
        /// Notify Screen Interactor that this screen is leaving.
        /// </summary>
        /// <returns>Screen exit parameters to save current state</returns>
        IDictionary<string, string> OnExit();
    }
}