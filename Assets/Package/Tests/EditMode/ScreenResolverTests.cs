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
    public class ScreenResolverTests
    {
        private IScreenResolver screenResolver;
        private IHistory history;
        private DiContainer container;

        [SetUp]
        public void Binding()
        {
            container = new DiContainer(StaticContext.Container);
            string appDomain = "domain://";

            container.Bind<IHistory>().FromInstance(Substitute.For<IHistory>()).AsSingle();
            container.Bind<IUrlManager>().To<UrlManager>().AsSingle();
            container.Bind<IScreenResolver>().To<ScreenResolver>().AsSingle();

            container.Bind<IUrlDomainProvider>().FromMethod(_ =>
            {
                var urlProvider = Substitute.For<IUrlDomainProvider>();
                urlProvider.Url.Returns(appDomain);
                return urlProvider;
            });

            container.Bind<IScreenFactory>()
               .FromMethod(_ =>
               {
                   var screenFactory = Substitute.For<IScreenFactory>();
                   screenFactory
                       .Create(Arg.Any<IScreenRegistry>())
                       .Returns(info => container.ResolveId<IScreenModel>(info.Arg<IScreenRegistry>().ScreenId));
                   return screenFactory;
               })
               .AsSingle();

            var registryB = Substitute.For<IScreenRegistry>();
            registryB.ScreenPresenter.Returns(typeof(ScreenBPresenter));
            registryB.ScreenId.Returns("MockScreenA");

            container.Bind<IScreenRegistry>().FromInstance(registryB).AsSingle();

            ScreenMocks.RegisterScreensModels(container);
            container.Inject(this);
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
            var firstScreen = container.Resolve<IScreenRegistry>().ScreenId;

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
            var name = screenResolver.GetScreenName(typeof(ScreenAPresenter));

            name.Should().Be("MockScreenA");
        }

        [Test]
        public void GetScreenName_InvalidType_ReturnStringEmpty()
        {
            var name = screenResolver.GetScreenName(typeof(UnregisteredScreenPresenter));

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
            model.Presenter.GetType().Should().Be(typeof(ScreenAPresenter));
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