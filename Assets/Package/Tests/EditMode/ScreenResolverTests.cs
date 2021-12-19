using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Installers;
using Elselam.UnityRouter.Url;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Zenject;

namespace Elselam.UnityRouter.Tests
{
    [TestFixture]
    public class ScreenResolverTests : ZenjectUnitTestFixture
    {
        private IScreenResolver screenResolver;
        private IHistory history;

        [SetUp]
        public void Binding()
        {
            string appDomain = "domain://";
            Container.Bind<IHistory>()
                .FromMethod(_ => Substitute.For<IHistory>())
                .AsSingle();

            Container.Bind<IUrlDomainProvider>().FromMethod(_ =>
            {
                var urlProvider = Substitute.For<IUrlDomainProvider>();
                urlProvider.Url.Returns(appDomain);
                return urlProvider;
            });

            Container.Bind<IUrlManager>()
                .To<UrlManager>()
                .AsSingle();

            var registryB = Substitute.For<IScreenRegistry>();
            registryB.ScreenInteractor.Returns(typeof(ScreenBInteractor));
            registryB.ScreenPresenter.Returns(typeof(ScreenBPresenter));
            registryB.ScreenId.Returns("MockScreenA");

            Container.Bind<IScreenRegistry>()
                .FromInstance(registryB)
                .AsSingle();

            Container.Bind<IScreenResolver>()
                .To<ScreenResolver>()
                .AsSingle();

            Container.Inject(this);
        }

        [Inject]
        public void Construct(IScreenResolver screenResolver, IHistory history)
        {
            this.screenResolver = screenResolver;
            this.history = history;
        }

        [Test]
        public void InitializeNavigation_LoadDefaultScreenAsFirstScreen()
        {
            var firstScreen = Container.Resolve<IScreenRegistry>().ScreenId;

            var result = screenResolver.ResolveScheme();

            result.ScreenId.Should().Be(firstScreen);
        }

        [Test]
        public void InitializeNavigation_WithHistory_LoadLastHistoryScreenAsFirstScreen()
        {
            var expectedFirstScreenName = "MockScreenB";
            history.HasHistory.Returns(true);
            history.Back().Returns(new ScreenScheme("", expectedFirstScreenName));

            var result = screenResolver.ResolveScheme();

            result.ScreenId.Should().Be(expectedFirstScreenName);
        }

        [Test]
        public void InitializeNavigation_WithDeepLink_LoadSpecifiedScreenAsFirstScreen()
        {
            var deeplink = "domain://MockScreenB?play_all=1&shuffle=1";
            screenResolver.ApplicationOnDeepLinkActivated(deeplink);

            var result = screenResolver.ResolveScheme();

            result.ScreenId.Should().Be("MockScreenB");
        }
    }
}