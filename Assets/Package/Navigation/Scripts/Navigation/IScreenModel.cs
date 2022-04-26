using Elselam.UnityRouter.Domain;
using UnityEngine;

namespace Elselam.UnityRouter.Installers
{
    public interface IScreenModel
    {
        /// <summary>
        /// Screen Name, also is the Screen Id
        /// </summary>
        string ScreenId { get; }

        /// <summary>
        /// Screen Presenter
        /// </summary>
        IScreenPresenter Presenter { get; }
    }
}