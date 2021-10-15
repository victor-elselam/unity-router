using System.Collections.Generic;
using elselam.Navigation.History;

namespace elselam.Navigation.Url {
    public interface IUrlManager {
        string BuildToString(string screenName, IDictionary<string, string> parameters);
        ScreenScheme BuildToScheme(string screenName, IDictionary<string, string> parameters);
        ScreenScheme Deserialize(string url);
    }
}