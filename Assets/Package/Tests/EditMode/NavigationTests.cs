using Cysharp.Threading.Tasks;
using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Installers;
using Elselam.UnityRouter.Installers;
using Elselam.UnityRouter.SceneLoad;
using Elselam.UnityRouter.Transitions;
using Elselam.UnityRouter.Url;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Elselam.UnityRouter.Tests
{
    [TestFixture]
    public class NavigationTests : ZenjectUnitTestFixture
    {

        private List<ScreenScheme> historyList;
        private ISceneLoader sceneLoader;
        private IHistory history;
        private IUrlManager urlManager;
        private INavigation navigation;

        [SetUp]
        public void Binding()
        {
            string appDomain = "domain://";

            historyList = new List<ScreenScheme>();
            Container.Bind<IHistory>()
                .FromMethod(_ =>
                {
                    var history = Substitute.For<IHistory>();
                    history.When(h => h.Add(Arg.Any<ScreenScheme>()))
                        .Do(callInfo => historyList.Add(callInfo.Arg<ScreenScheme>()));
                    history.Add(Arg.Any<ScreenScheme>()).Returns(true);
                    return history;
                })
                .AsSingle();

            Container.Bind<IScreenFactory<IScreenRegistry, IScreenModel>>()
                .FromMethod(_ =>
                {
                    var screenFactory = Substitute.For<IScreenFactory<IScreenRegistry, IScreenModel>>();
                    screenFactory
                        .Create(Arg.Any<IScreenRegistry>())
                        .Returns(info => Container.ResolveId<IScreenModel>(info.Arg<IScreenRegistry>().ScreenId));
                    return screenFactory;
                })
                .AsSingle();

            //Container.BindFactory<IScreenRegistry, IScreenModel, ScreenFactory>().FromMethod((container, registry)
            //    => Container.ResolveId<IScreenModel>(registry.ScreenId));

            Container.Bind<ISceneLoader>()
                .FromMethod(_ => Substitute.For<ISceneLoader>())
                .AsSingle();

            Container.Bind<IScreenResolver>()
                .FromMethod(_ =>
                {
                    var resolver = Substitute.For<IScreenResolver>();
                    resolver.ResolveScheme().Returns(_ => new ScreenScheme("", "MockScreenA"));
                    return resolver;
                })
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

            Container.Bind<ITransition>().FromMethod(_ =>
            {
                var transition = Substitute.For<ITransition>();
                transition.Transite(Arg.Any<IScreenPresenter>(), Arg.Any<IScreenPresenter>())
                    .Returns(info => UniTask.CompletedTask);
                return transition;
            }).AsSingle();

            RegisterScreensModels();

            Container.Bind<IScreenRegistry>()
                .FromMethod(_ =>
                {
                    var defaultScreen = Substitute.For<IScreenRegistry>();
                    defaultScreen.ScreenId.Returns("MockScreenA");
                    defaultScreen.ScreenInteractor.Returns(typeof(ScreenAInteractor));
                    defaultScreen.ScreenPresenter.Returns(typeof(ScreenAPresenter));
                    return defaultScreen;
                })
                .AsSingle();

            Container.Bind<INavigation>()
                .To<NavigationManager>()
                .AsSingle();

            Container.Inject(this);
        }

        [Inject]
        public void Construct(ISceneLoader sceneLoader, IHistory history, IUrlManager urlManager, INavigation navigation)
        {
            this.sceneLoader = sceneLoader;
            this.history = history;
            this.urlManager = urlManager;
            this.navigation = navigation;

            navigation.Initialize();
        }

        [Test]
        public void NavigateTo_ValidScreen_Success()
        {
            navigation.NavigateTo<ScreenAInteractor>();

            navigation.CurrentScreen.Screen.Should().NotBeNull();
            navigation.CurrentScreen.Screen.GetType().Should().Be(typeof(ScreenAInteractor));
        }

        [Test]
        public void NavigateTo_ValidScreen_ShouldCarryScreenInfos()
        {
            var customScreen = Container.Resolve<CustomScreenInteractor>();
            var parameters = new Dictionary<string, string> {
                {"ItemLength", "15"},
                {"ItemPosition", JsonUtility.ToJson(new Vector3(15f, 10f, 5f))},
            };
            navigation.NavigateTo<ScreenAInteractor>();

            navigation.NavigateTo<CustomScreenInteractor>(parameters: parameters);

            customScreen.ItemLength.Should().NotBe(0);
            customScreen.ItemPosition.Should().NotBe(Vector3.zero);
        }

        [Test]
        public void NavigateTo_InvalidScreen_Failure()
        {
            NavigationException error = null;

            try
            {
                navigation.NavigateTo<UnregisteredScreenInteractor>();
            }
            catch (NavigationException e)
            {
                error = e;
            }

            error.Should().NotBeNull();
        }

        [Test]
        public void NavigateTo_EmptyScreenId_Failure()
        {
            NavigationException error = null;

            try
            {
                navigation.NavigateTo<EmptyIdInteractor>();
            }
            catch (NavigationException e)
            {
                error = e;
            }

            error.Should().NotBeNull();
        }

        [Test]
        public void NavigateTo_UsingValidDeepLink_Success()
        {
            var urlProvider = Container.Resolve<IUrlDomainProvider>();
            var url = $"{urlProvider.Url}MockScreenA?test=true";
            var scheme = urlManager.Deserialize(url);

            navigation.NavigateTo(scheme);

            navigation.CurrentScreen.Scheme.ScreenId.Should().Be("MockScreenA");
            navigation.CurrentScreen.Scheme.Parameters["test"].Should().Be("true");
        }

        [Test]
        public void NavigateTo_UsingInvalidDeepLink_ThrowNavigationException()
        {
            NavigationException exception = null;
            var url = "domain://play_group/forum/question?play_all=1&shuffle=1";
            var scheme = urlManager.Deserialize(url);

            try
            {
                navigation.NavigateTo(scheme);
            }
            catch (NavigationException e)
            {
                exception = e;
            }

            exception.Should().NotBeNull();
        }

        [Test]
        public void BackToLastScreen_WithFilledHistory_Success()
        {
            var history = Container.Resolve<IHistory>();
            history.Back().Returns(new ScreenScheme("", "MockScreenA"));
            var transition = Container.Resolve<ITransition>();

            navigation.BackToLastScreen(transition);

            navigation.CurrentScreen.Screen.Should().NotBeNull();
            navigation.CurrentScreen.Screen.GetType().Should().Be(typeof(ScreenAInteractor));
        }

        [Test]
        public void BackToLastScreen_WithEmptyHistory_Failure()
        {
            NavigationException error = null;
            var history = Container.Resolve<IHistory>();
            history.Back().Returns((_ => null));
            var transition = Container.Resolve<ITransition>();

            try
            {
                navigation.BackToLastScreen(transition);
            }
            catch (NavigationException e)
            {
                error = e;
            }

            error.Should().NotBeNull();
        }

        [Test]
        public void BackToLastScreen_ValidScreen_ShouldCarryScreenInfos()
        {
            var history = Container.Resolve<IHistory>();
            var customScreen = Container.Resolve<CustomScreenInteractor>();
            navigation.NavigateTo<CustomScreenInteractor>();
            navigation.NavigateTo<ScreenAInteractor>();
            history.Back().Returns(_ => historyList.Last());

            navigation.BackToLastScreen();

            customScreen.ItemLength.Should().NotBe(0);
            customScreen.ItemPosition.Should().NotBe(Vector3.zero);
        }

        [Test]
        public void LoadScene_CallSceneLoaderWithLoadScreen()
        {
            var sceneLoadCalled = false;
            var loadLoadingSceneCalled = false;
            var unloadLoadingSceneCalled = false;
            var sceneName = "TestScene";
            navigation.NavigateTo<ScreenAInteractor>();
            sceneLoader
                .When(s => s.LoadLoadingScene())
                .Do(_ => loadLoadingSceneCalled = true);
            sceneLoader
                .When(s => s.LoadScene(sceneName))
                .Do(_ => sceneLoadCalled = true);
            sceneLoader
                .When(s => s.UnloadLoadingScene())
                .Do(_ => unloadLoadingSceneCalled = true);

            navigation.NavigateTo(sceneName);

            loadLoadingSceneCalled.Should().BeTrue();
            sceneLoadCalled.Should().BeTrue();
            unloadLoadingSceneCalled.Should().BeTrue();
        }

        [Test]
        public void LoadScene_AssignCurrentSchemeToSpecifiedScene()
        {
            var sceneName = "TestScene";
            navigation.NavigateTo<ScreenAInteractor>();

            navigation.NavigateTo(sceneName);

            navigation.CurrentScreen.Scheme.ScreenId.Should().Be("TestScene");
        }

        [Test]
        public void LoadScene_LeavingScreen_AddScreenSchemeToHistory()
        {
            var addedToHistory = false;
            navigation.NavigateTo<ScreenAInteractor>();
            history
                .When(history => history.Add(Arg.Any<ScreenScheme>()))
                .Do(info => addedToHistory = true);

            navigation.NavigateTo("");

            addedToHistory.Should().BeTrue();
        }

        [Test]
        public void BackFromScene_CallSceneLoaderWithLoadScreen()
        {
            var mainSceneLoadCalled = false;
            var loadLoadingSceneCalled = false;
            var unloadLoadingSceneCalled = false;
            history.Back().Returns(_ => new ScreenScheme("", "MockScreenA"));
            sceneLoader
                .When(s => s.LoadLoadingScene())
                .Do(_ => loadLoadingSceneCalled = true);
            sceneLoader
                .When(s => s.LoadMainScene())
                .Do(_ => mainSceneLoadCalled = true);
            sceneLoader
                .When(s => s.UnloadLoadingScene())
                .Do(_ => unloadLoadingSceneCalled = true);

            navigation.BackToMainScene();

            loadLoadingSceneCalled.Should().BeTrue();
            mainSceneLoadCalled.Should().BeTrue();
            unloadLoadingSceneCalled.Should().BeTrue();
        }

        [Test]
        public void BackFromScene_ReactivateLastScreen()
        {
            history.Back().Returns(_ => new ScreenScheme("", "MockScreenA"));

            navigation.BackToMainScene();

            navigation.CurrentScreen.Scheme.ScreenId.Should().Be("MockScreenA");
        }

        [Test]
        public void NavigateTo_SameScreen_CallOnExitAddToHistoryAndCallOnEnter()
        {
            var sameScreen = Container.Resolve<SameScreenInteractor>();
            var parameters = new Dictionary<string, string> { ["PageIndex"] = 2.ToString() };

            navigation.NavigateTo<SameScreenInteractor>(parameters: parameters);
            parameters["PageIndex"] = 5.ToString();
            navigation.NavigateTo<SameScreenInteractor>(parameters: parameters);
            navigation.NavigateTo<CustomScreenInteractor>();

            sameScreen.OnEnterCalled.Should().Be(2);
            historyList.Any(hl => hl.Url.Contains("PageIndex=2")).Should().BeTrue();
            historyList.Any(hl => hl.Url.Contains("PageIndex=5")).Should().BeTrue();
        }

        [Test]
        public void BackToLastScreen_LastIsASceneScheme_SetCurrentSceneScheme()
        {
            var screenAInteractor = Container.ResolveId<IScreenModel>("MockScreenA").Interactor;
            navigation.CurrentScreen.SetCurrentScreen(screenAInteractor, new ScreenScheme(null, "MockScreenA"));
            history.Back().Returns(new SceneScheme(null, "testScene"));

            navigation.BackToLastScreen();

            navigation.CurrentScreen.Scheme.Should().BeOfType(typeof(SceneScheme));
            navigation.CurrentScreen.Scheme.ScreenId.Should().Be("testScene");
        }

        [Test]
        public void BackToLastScreen_LastIsASceneScheme_CallExitInCurrentScreen()
        {
            var onExitInteractor = Container.Resolve<OnExitInteractor>();
            navigation.CurrentScreen.SetCurrentScreen(onExitInteractor, new ScreenScheme(null, "MockScreenA"));
            history.Back().Returns(new SceneScheme(null, "testScene"));

            navigation.BackToLastScreen();

            onExitInteractor.OnExitCalled.Should().Be(1);
        }

        private List<IScreenRegistry> MockScreenRegistry()
        {
            var registryA = Container.ResolveId<IScreenRegistry>("MockScreenA");
            var registryB = Container.ResolveId<IScreenRegistry>("MockScreenB");
            var registryCustom = Container.ResolveId<IScreenRegistry>("MockScreenCustom");
            return new List<IScreenRegistry> { registryA, registryB, registryCustom };
        }

        private void RegisterScreensModels()
        {
            var registryA = Substitute.For<IScreenRegistry>();
            registryA.ScreenInteractor.Returns(typeof(ScreenAInteractor));
            registryA.ScreenPresenter.Returns(typeof(ScreenAPresenter));
            registryA.ScreenId.Returns("MockScreenA");
            Container.Bind<IScreenRegistry>().WithId("MockScreenA").FromInstance(registryA);
            Container.Bind<ScreenAInteractor>()
                .AsSingle();
            Container.Bind<ScreenAPresenter>()
                .AsSingle();
            Container.Bind<IScreenModel>().WithId("MockScreenA").FromMethod(_ =>
                new ScreenModel("MockScreenA",
                    Container.Resolve<ScreenAInteractor>(),
                    Container.Resolve<ScreenAPresenter>(),
                    null));

            var registryB = Substitute.For<IScreenRegistry>();
            registryB.ScreenInteractor.Returns(typeof(ScreenBInteractor));
            registryB.ScreenPresenter.Returns(typeof(ScreenBPresenter));
            registryB.ScreenId.Returns("MockScreenB");
            Container.Bind<IScreenRegistry>().WithId("MockScreenB").FromInstance(registryB);
            Container.Bind<ScreenBInteractor>()
                .AsSingle();
            Container.Bind<ScreenBPresenter>()
                .AsSingle();
            Container.Bind<IScreenModel>().WithId("MockScreenB").FromMethod(_ =>
                new ScreenModel("MockScreenB",
                    Container.Resolve<ScreenBInteractor>(),
                    Container.Resolve<ScreenBPresenter>(),
                    null));

            var registryCustom = Substitute.For<IScreenRegistry>();
            registryCustom.ScreenInteractor.Returns(typeof(CustomScreenInteractor));
            registryCustom.ScreenPresenter.Returns(typeof(CustomScreenPresenter));
            registryCustom.ScreenId.Returns("MockScreenCustom");
            Container.Bind<IScreenRegistry>().WithId("MockScreenCustom").FromInstance(registryCustom);
            Container.Bind<CustomScreenInteractor>()
                .AsSingle();
            Container.Bind<CustomScreenPresenter>()
                .AsSingle();
            Container.Bind<IScreenModel>().WithId("MockScreenCustom").FromMethod(_ =>
                new ScreenModel("MockScreenCustom",
                Container.Resolve<CustomScreenInteractor>(),
                Container.Resolve<CustomScreenPresenter>(),
                null));

            var emptyIdRegistry = Substitute.For<IScreenRegistry>();
            emptyIdRegistry.ScreenInteractor.Returns(typeof(EmptyIdInteractor));
            emptyIdRegistry.ScreenPresenter.Returns(typeof(EmptyIdPresenter));
            emptyIdRegistry.ScreenId.Returns(string.Empty);
            Container.Bind<IScreenRegistry>().WithId("EmptyIdScreenCustom").FromInstance(emptyIdRegistry);
            Container.Bind<EmptyIdInteractor>()
                .AsSingle();
            Container.Bind<EmptyIdPresenter>()
                .AsSingle();
            Container.Bind<IScreenModel>().WithId(string.Empty).FromMethod(_ =>
                new ScreenModel("EmptyIdScreenCustom",
                    Container.Resolve<EmptyIdInteractor>(),
                    Container.Resolve<EmptyIdPresenter>(),
                    null));

            var sameIdRegistry = Substitute.For<IScreenRegistry>();
            sameIdRegistry.ScreenInteractor.Returns(typeof(SameScreenInteractor));
            sameIdRegistry.ScreenPresenter.Returns(typeof(SameScreenPresenter));
            sameIdRegistry.ScreenId.Returns("MockSameScreen");
            Container.Bind<IScreenRegistry>().WithId("MockSameScreen").FromInstance(sameIdRegistry);
            Container.Bind<SameScreenInteractor>()
                .AsSingle();
            Container.Bind<SameScreenPresenter>()
                .AsSingle();
            Container.Bind<IScreenModel>().WithId("MockSameScreen").FromMethod(_ =>
                new ScreenModel("MockSameScreen",
                    Container.Resolve<SameScreenInteractor>(),
                    Container.Resolve<SameScreenPresenter>(),
                    null));

            var onExitRegistry = Substitute.For<IScreenRegistry>();
            onExitRegistry.ScreenInteractor.Returns(typeof(OnExitInteractor));
            onExitRegistry.ScreenPresenter.Returns(typeof(OnExitPresenter));
            onExitRegistry.ScreenId.Returns("MockExitScreen");
            Container.Bind<IScreenRegistry>().WithId("MockExitScreen").FromInstance(onExitRegistry);
            Container.Bind<OnExitInteractor>()
                     .AsSingle();
            Container.Bind<OnExitPresenter>()
                     .AsSingle();
            Container.Bind<IScreenModel>().WithId("MockExitScreen").FromMethod(_ =>
                new ScreenModel("MockExitScreen",
                    Container.Resolve<OnExitInteractor>(),
                    Container.Resolve<OnExitPresenter>(),
                    null));

            var registries = new List<IScreenRegistry> { registryA, registryB, registryCustom, emptyIdRegistry, sameIdRegistry, onExitRegistry };
            Container.Bind<List<IScreenRegistry>>()
                .FromInstance(registries)
                .AsSingle();
        }
    }
}