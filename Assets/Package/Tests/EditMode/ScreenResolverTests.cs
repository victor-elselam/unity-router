using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Installers;
using Elselam.UnityRouter.Tests.Mocks;
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

            Container.Bind<IScreenFactory>()
               .FromMethod(_ =>
               {
                   var screenFactory = Substitute.For<IScreenFactory>();
                   screenFactory
                       .Create(Arg.Any<IScreenRegistry>())
                       .Returns(info => Container.ResolveId<IScreenModel>(info.Arg<IScreenRegistry>().ScreenId));
                   return screenFactory;
               })
               .AsSingle();

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

            ScreenMocks.RegisterScreensModels(Container);
            Container.Inject(this);
        }

        [Inject]
        public void Construct(IScreenResolver screenResolver, IHistory history)
        {
            this.screenResolver = screenResolver;
            this.history = history;

            screenResolver.Initialize();
        }

        [Test]
        public void InitializeNavigation_LoadDefaultScreenAsFirstScreen()
        {
            var firstScreen = Container.Resolve<IScreenRegistry>().ScreenId;

            var result = screenResolver.ResolveFirstScreen();

            result.ScreenId.Should().Be(firstScreen);
        }

        [Test]
        public void InitializeNavigation_WithDeepLink_LoadSpecifiedScreenAsFirstScreen()
        {
            var deeplink = "domain://MockScreenB?play_all=1&shuffle=1";
            screenResolver.ApplicationOnDeepLinkActivated(deeplink);

            var result = screenResolver.ResolveFirstScreen();

            result.ScreenId.Should().Be("MockScreenB");
        }

        [Test]
        public void GetScreenName_ValidType_ReturnScreenName()
        {
            var name = screenResolver.GetScreenName(typeof(ScreenAInteractor));

            name.Should().Be("MockScreenA");
        }

        [Test]
        public void GetScreenName_InvalidType_ReturnStringEmpty()
        {
            var name = screenResolver.GetScreenName(typeof(UnregisteredScreenInteractor));

            name.Should().BeNull();
        }

        [Test]
        public void GetScreenModel_InvalidScreen_ReturnNull()
        {
            var model = screenResolver.GetScreenModel("invalidScreenName");

            model.Should().BeNull();
        }

        [Test]
        public void GetScreenModel_EmptyName_ReturnNull()
        {
            var model = screenResolver.GetScreenModel(string.Empty);

            model.Should().BeNull();
        }

        [Test]
        public void GetScreenModel_ValidScreen_ReturnModel()
        {
            var model = screenResolver.GetScreenModel("MockScreenA");

            model.Should().NotBeNull();
            model.ScreenId.Should().Be("MockScreenA");
            model.Interactor.GetType().Should().Be(typeof(ScreenAInteractor));
        }

        [Test]
        public void InitializeNavigation_WithHistory_LoadLastHistoryScreenAsFirstScreen()
        {
            var expectedFirstScreenName = "MockScreenB";
            history.HasHistory.Returns(true);
            history.Back().Returns(new ScreenScheme("", expectedFirstScreenName));

            var result = screenResolver.ResolveFirstScreen();

            result.ScreenId.Should().Be(expectedFirstScreenName);
        }
    }
}