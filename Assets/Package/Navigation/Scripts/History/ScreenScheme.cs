using System.Collections.Generic;

namespace Elselam.UnityRouter.History
{
    /// <summary>
    /// Scheme to hold screen metadata while stored in History
    /// </summary>
    public class ScreenScheme
    {
        public ScreenScheme(string url, string screenId, IDictionary<string, string> parameters = null)
        {
            Url = url;
            ScreenId = screenId;
            Parameters = parameters;
        }

        public ScreenScheme(string url, string screenId)
        {
            Url = url;
            ScreenId = screenId;
        }

        public string Url;
        public string ScreenId;
        public IDictionary<string, string> Parameters;
    }
}