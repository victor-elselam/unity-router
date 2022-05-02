using Elselam.UnityRouter.History;
using FluentAssertions;
using NUnit.Framework;
using Zenject;


namespace Elselam.UnityRouter.Tests
{
    [TestFixture]
    public class HistoryTests
    {
        private DiContainer container;

        [SetUp]
        public void Binding()
        {
            container = new DiContainer(StaticContext.Container);

            container.Bind<IHistory>()
                .To<HistoryManager>()
                .AsSingle();
        }

        [Test]
        public void Add_ValidScreenScheme_ReturnSuccess()
        {
            var scheme = new ScreenScheme("://domain.com/MockScreenA", "MockScreenA");
            var history = container.Resolve<IHistory>();

            var added = history.Add(scheme);

            added.Should().BeTrue();
        }

        [Test]
        public void Add_NullScreenScheme_ReturnFail()
        {
            ScreenScheme scheme = null;
            var history = container.Resolve<IHistory>();

            var added = history.Add(scheme);

            added.Should().BeFalse();
        }

        [Test]
        [TestCase("MockScreenA", "MockScreenB", "MockScreenC")]
        [TestCase("MockScreenB", "MockScreenC", "MockScreenD")]
        [TestCase("MockScreenC", "MockScreenD", "MockScreenA")]
        public void Back_ThreeTimes_CheckScreenSuccess(string screen1, string screen2, string screen3)
        {
            ScreenScheme scheme = null;
            var history = container.Resolve<IHistory>();
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
            var history = container.Resolve<IHistory>();
            var scheme1 = new ScreenScheme("", "MockScreenA");
            var scheme2 = new ScreenScheme("", "MockScreenB");
            history.Add(scheme1);
            history.Add(scheme2);

            history.Back();
            history.Back();
            var screenScheme = history.Back();

            screenScheme.Should().BeNull();
        }

        [Test]
        public void CloseSubflow_InMainFlow_ReturnFalse()
        {
            var history = container.Resolve<IHistory>();
            var scheme1 = new ScreenScheme("", "MockScreenA");
            var scheme2 = new ScreenScheme("", "MockScreenB");
            history.Add(scheme1);
            history.Add(scheme2);

            var result = history.CloseSubflow();

            result.Should().BeFalse();
        }

        [Test]
        public void CloseSubflow_InOtherFlow_ReturnTrue()
        {
            var history = container.Resolve<IHistory>();
            var scheme1 = new ScreenScheme("", "MockScreenA");
            var subScheme1 = new ScreenScheme("", "MockScreen1Subflow");
            var subScheme2 = new ScreenScheme("", "MockScreen2Subflow");
            history.Add(scheme1);
            history.OpenSubflow();
            history.Add(subScheme1);
            history.Add(subScheme2);

            var result = history.CloseSubflow();

            result.Should().BeTrue();
        }

        [Test]
        public void Back_OpenSubflowNavigateCloseSubflow_ReturnScreenBeforeOfOpenFlow()
        {
            var history = container.Resolve<IHistory>();
            var scheme1 = new ScreenScheme("", "MockScreenA");
            var subScheme1 = new ScreenScheme("", "MockScreen1Subflow");
            var subScheme2 = new ScreenScheme("", "MockScreen2Subflow");
            history.Add(scheme1);
            history.OpenSubflow();
            history.Add(subScheme1);
            history.Add(subScheme2);
            history.CloseSubflow();

            var screenScheme = history.Back();

            screenScheme.ScreenId.Should().Be(scheme1.ScreenId);
        }

        [Test]
        public void Back_DuringSubflow_ReturnLastScreenOfCurrentSubflow()
        {
            var history = container.Resolve<IHistory>();
            var scheme1 = new ScreenScheme("", "MockScreenA");
            var subScheme1 = new ScreenScheme("", "MockScreen1Subflow");
            history.Add(scheme1);
            history.OpenSubflow();
            history.Add(subScheme1);

            var screenScheme = history.Back();

            screenScheme.ScreenId.Should().Be(subScheme1.ScreenId);
        }
    }
}