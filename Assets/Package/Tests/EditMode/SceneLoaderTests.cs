using Elselam.UnityRouter.History;
using Elselam.UnityRouter.SceneLoad;
using NUnit.Framework;
using System.Collections.Generic;
using Zenject;

namespace Elselam.UnityRouter.Tests
{
    [TestFixture]
    public class SceneLoaderTests
    {
        private const string validScene = "ValidScreen";
        private const string invalidScene = "InvalidScreen";

        private ISceneLoader sceneLoader;

        [SetUp]
        public void Binding()
        {
            var container = new DiContainer(StaticContext.Container);
            container.Inject(this);
        }

        [Inject]
        public void Construct()
        {

        }

        [Test]
        public void LoadScreen_ValidScreen_ScreenReceiveOnEnterCall()
        {
            
        }
    }
}