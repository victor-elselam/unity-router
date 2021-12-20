using System.Collections.Generic;

namespace Elselam.UnityRouter.History
{
    /// <summary>
    /// Scheme used to navigate between scenes. This is a feature for projects that still depends on some scenes, even that it's not recommended.
    /// </summary>
    public class SceneScheme : ScreenScheme
    {
        public SceneScheme() : base("", "") { }

        public SceneScheme(string url, string sceneId, IDictionary<string, string> parameters = null) : base(url, sceneId, parameters) { }
    }
}
