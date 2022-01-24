using Cysharp.Threading.Tasks;
using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Installers;
using Elselam.UnityRouter.ScreenLoad;
using Elselam.UnityRouter.Transitions;
using Elselam.UnityRouter.Url;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.TestTools;
using Zenject;

namespace Elselam.UnityRouter.Tests
{
    [TestFixture]
    public class ScreenLoaderTests : ZenjectUnitTestFixture
    {
        private List<ScreenScheme> historyList;
        private const string validScreen = "ValidScreen";
        private const string invalidScreen = "InvalidScreen";

        private IScreenLoader screenLoader;
        private IScreenInteractor screenInteractor;
        private ITransition transition;

        [SetUp]
        public void Binding()
        {
            Container.Bind<IScreenInteractor>()
                .FromMethod(_ => Substitute.For<IScreenInteractor>())
                .AsSingle();
            Container.Bind<IScreenPresenter>()
                .FromMethod(_ => Substitute.For<IScreenPresenter>())
                .AsSingle();

            Container.Bind<IScreenResolver>()
                .FromMethod(_ =>
                {
                    var resolver = Substitute.For<IScreenResolver>();
                    resolver.ResolveFirstScreen().Returns(_ => new ScreenScheme("", "MockScreenA"));
                    resolver.GetScreenModel(Arg.Any<string>()).Returns(info =>
                    {
                        if (info.Arg<string>() == validScreen)
                        {
                            return new ScreenModel(info.Arg<string>(),
                                Container.Resolve<IScreenInteractor>(),
                                Container.Resolve<IScreenPresenter>(),
                                null);
                        }

                        return null;
                    });

                    return resolver;
                }).AsSingle();

            Container.Bind<ITransition>()
                .FromMethod(_ =>
                {
                    var transition = Substitute.For<ITransition>();
                    transition.Transite(Arg.Any<IScreenPresenter>(), Arg.Any<IScreenPresenter>())
                        .Returns(info => UniTask.CompletedTask);
                    return transition;
                }).AsSingle();

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
                }).AsSingle();

            Container.Bind<IUrlDomainProvider>()
                .FromMethod(_ =>
                {
                    var urlProvider = Substitute.For<IUrlDomainProvider>();
                    urlProvider.Url.Returns(appDomain);
                    return urlProvider;
                });

            Container.Bind<IUrlManager>()
                .To<UrlManager>()
                .AsSingle();

            Container.Bind<IScreenLoader>()
               .To<ScreenLoader>()
               .AsSingle();

            Container.Inject(this);
        }

        [Inject]
        public void Construct(IScreenLoader screenLoader,
            IScreenInteractor screenInteractor,
            ITransition transition)
        {
            this.screenLoader = screenLoader;
            this.screenInteractor = screenInteractor;
            this.transition = transition;
        }

        [Test]
        public void LoadScreen_ValidScreen_ScreenReceiveOnEnterCall()
        {
            screenLoader.LoadScreen(GetValidScreenScheme());

            screenInteractor.Received(1).OnEnter(null);
        }

        [Test]
        public void LoadScreen_ValidScreenWithParameters_ScreenReceiveWithParametersCall()
        {
            var parameters = new Dictionary<string, string> { ["foo"] = "bar" };
            screenLoader.LoadScreen(GetValidScreenScheme(parameters));

            screenInteractor.Received(1).OnEnter(parameters);
        }

        [UnityTest]
        public IEnumerator LoadScreen_InvalidScreen_Failure() => UniTask.ToCoroutine(async () =>
        {
            NavigationException error = null;

            try
            {
                await screenLoader.LoadScreen(GetInvalidScreenScheme());
            }
            catch (NavigationException e)
            {
                error = e;
            }

            error.Should().NotBeNull();
        });

        [UnityTest]
        public IEnumerator LoadScreen_EmptyScreenId_Failure() => UniTask.ToCoroutine(async () =>
        {
            NavigationException error = null;

            try
            {
                await screenLoader.LoadScreen(GetEmptyScreenScheme());
            }
            catch (NavigationException e)
            {
                error = e;
            }

            error.Should().NotBeNull();
        });

        [Test]
        public void Transition_SameScreen_DontTransite()
        {
            screenLoader.Transition(GetValidScreenScheme(), GetValidScreenScheme());

            transition.Received(0).Transite(Arg.Any<IScreenPresenter>(), Arg.Any<IScreenPresenter>());
        }

        [Test]
        public void UnloadScreen_WithoutBack_ReturnExitParameters()
        {
            var parameters = new Dictionary<string, string> { ["foo"] = "bar" };
            screenInteractor.OnExit().Returns(_ => parameters);

            var scheme = screenLoader.UnloadScreen(GetValidScreenScheme());

            scheme.Parameters["foo"].Should().Be("bar");
            scheme.ScreenId.Should().Be(validScreen);
        }

        [Test]
        public void UnloadScreen_WithBack_ReturnNull()
        {
            var parameters = new Dictionary<string, string> { ["foo"] = "bar" };
            screenInteractor.OnExit().Returns(_ => parameters);

            var scheme = screenLoader.UnloadScreen(GetValidScreenScheme(), true);

            scheme.Should().BeNull();
        }

        [Test]
        public void UnloadScreen_WithNullScheme_ReturnNull()
        {
            var scheme = screenLoader.UnloadScreen(null);

            scheme.Should().BeNull();
        }

        private ScreenScheme GetValidScreenScheme(IDictionary<string, string> parameters = null) => new ScreenScheme("", validScreen, parameters);
        private ScreenScheme GetInvalidScreenScheme(IDictionary<string, string> parameters = null) => new ScreenScheme("", invalidScreen, parameters);
        private ScreenScheme GetEmptyScreenScheme(IDictionary<string, string> parameters = null) => new ScreenScheme("", "", parameters);
    }
}