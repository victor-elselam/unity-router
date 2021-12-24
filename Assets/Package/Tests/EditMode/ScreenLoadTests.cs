using Elselam.UnityRouter.ScreenLoad;
using NUnit.Framework;
using Zenject;

namespace Elselam.UnityRouter.Tests
{
    [TestFixture]
    public class ScreenLoadTests : ZenjectUnitTestFixture
    {
        private IScreenLoader screenLoader;

        [SetUp]
        public void Binding()
        {
            Container.Bind<IScreenLoader>()
                .To<DIScreenLoader>()
                .AsSingle();

            Container.Inject(this);
        }

        [Inject]
        public void Construct(IScreenLoader screenLoader)
        {
            this.screenLoader = screenLoader;
        }

        [Test]
        public void Test()
        {
           
        }
    }
}