using Elselam.UnityRouter.History;
using System.Collections.Generic;

namespace Elselam.UnityRouter.Url
{
    public interface IUrlManager
    {
        /// <summary>
        /// Build specified screen and parameters to a deep link, interpreted by INavigation classes
        /// </summary>
        /// <param name="screenName"></param>
        /// <param name="parameters"></param>
        /// <returns>Built deep link</returns>
        string BuildToString(string screenName, IDictionary<string, string> parameters);

        /// <summary>
        /// Build specified screen and parameters to a ScreenScheme, used to perform navigation
        /// </summary>
        /// <param name="screenName"></param>
        /// <param name="parameters"></param>
        /// <returns>Built ScreenScheme</returns>
        ScreenScheme BuildToScheme(string screenName, IDictionary<string, string> parameters);

        /// <summary>
        /// Transform a deep link into ScreenScheme
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Deserialized ScreenScheme</returns>
        ScreenScheme Deserialize(string url);
    }
}