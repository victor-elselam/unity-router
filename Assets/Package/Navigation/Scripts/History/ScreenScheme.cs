using System.Collections.Generic;

namespace elselam.Navigation.History {
    public class ScreenScheme {
        public ScreenScheme(string url, string screenId, IDictionary<string, string> parameters = null) {
            Url = url;
            ScreenId = screenId;
            Parameters = parameters;
        }

        public string Url;
        public string ScreenId;
        public IDictionary<string, string> Parameters;
    }
}