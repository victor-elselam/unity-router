using NUnit.Framework;
using Zenject;

namespace Elselam.UnityRouter.Tests
{
    [TestFixture]
    public class SceneLoadTests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void Binding()
        {
            Container.Inject(this);
        }

        [Inject]
        public void Construct()
        {
            
        }

        [Test]
        public void Method_Scenery_ExpectedResult()
        {
            
        }

        [Test]
        public void LoadScene_AssignCurrentSchemeToSpecifiedScene()
        {
            //navigation.NavigateTo<ScreenAInteractor>();

            //navigation.NavigateTo("TestScene");

            //currentScreen.Scheme.ScreenId.Should().Be("TestScene");
        }
    }
}