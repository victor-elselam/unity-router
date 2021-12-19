using Elselam.UnityRouter.History;
using FluentAssertions;
using NUnit.Framework;
using Zenject;


namespace Elselam.UnityRouter.Tests
{
    [TestFixture]
    public class HistoryTests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void Binding()
        {
            Container.Bind<IHistory>()
                .To<HistoryManager>()
                .AsSingle();
        }

        [Test]
        public void Add_ValidScreenScheme_ReturnSuccess()
        {
            var added = false;
            var scheme = new ScreenScheme("://domain.com/MockScreenA", "MockScreenA");
            var history = Container.Resolve<IHistory>();

            added = history.Add(scheme);

            added.Should().BeTrue();
        }

        [Test]
        public void Add_NullScreenScheme_ReturnFail()
        {
            var added = false;
            ScreenScheme scheme = null;
            var history = Container.Resolve<IHistory>();

            added = history.Add(scheme);

            added.Should().BeFalse();
        }

        [Test]
        [TestCase("MockScreenA", "MockScreenB", "MockScreenC")]
        [TestCase("MockScreenB", "MockScreenC", "MockScreenD")]
        [TestCase("MockScreenC", "MockScreenD", "MockScreenA")]
        public void Back_ThreeTimes_CheckScreenSuccess(string screen1, string screen2, string screen3)
        {
            ScreenScheme scheme = null;
            var history = Container.Resolve<IHistory>();
            history.Add(new ScreenScheme("", screen1));
            history.Add(new ScreenScheme("", screen2));
            history.Add(new ScreenScheme("", screen3));

            history.Back();
            history.Back();
            scheme = history.Back();

            scheme.ScreenId.Should().Be(screen1);
        }

        [Test]
        public void Back_ThreeTimesFails_ReturnNull()
        {
            var history = Container.Resolve<IHistory>();
            var scheme1 = new ScreenScheme("", "MockScreenA");
            var scheme2 = new ScreenScheme("", "MockScreenB");
            history.Add(scheme1);
            history.Add(scheme2);
            var screenScheme = scheme2;

            history.Back();
            history.Back();
            screenScheme = history.Back();

            screenScheme.Should().BeNull();
        }
    }
}