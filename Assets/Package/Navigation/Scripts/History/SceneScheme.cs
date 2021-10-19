using System.Collections.Generic;
using elselam.Navigation.History;

namespace elselam.Navigation.History {
    public class SceneScheme : ScreenScheme {
        public SceneScheme() : base("", "") { }
        
        public SceneScheme(string url, string screenId, IDictionary<string, string> parameters = null) : base(url, screenId, parameters) { }
    }
}
