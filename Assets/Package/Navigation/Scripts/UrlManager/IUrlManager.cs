using Elselam.UnityRouter.History;
using System.Collections.Generic;

namespace Elselam.UnityRouter.Url
{
    public interface IUrlManager
    {
        string BuildToString(string screenName, IDictionary<string, string> parameters);
        ScreenScheme BuildToScheme(string screenName, IDictionary<string, string> parameters);
        ScreenScheme Deserialize(string url);
    }
}