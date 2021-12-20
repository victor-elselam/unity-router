using System.Collections.Generic;

namespace Elselam.UnityRouter.Domain
{
    public interface IScreenInteractor
    {
        /// <summary>
        /// Notify Screen Interactor that this screen is entering.
        /// At this call, screen parameters is already available
        /// </summary>
        void OnEnter();

        /// <summary>
        /// Notify Screen Interactor that this screen is leaving.
        /// </summary>
        /// <returns>Screen exit parameters to save current state</returns>
        IDictionary<string, string> OnExit();

        /// <summary>
        /// Inject parameters from last screen or deep link
        /// </summary>
        /// <param name="parameters"></param>
        void WithParameters(IDictionary<string, string> parameters);
    }
}