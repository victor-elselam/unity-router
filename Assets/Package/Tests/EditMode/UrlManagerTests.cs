using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Url;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using Zenject;

namespace Elselam.UnityRouter.Tests
{
    [TestFixture]
    public class UrlManagerTests
    {
        private IUrlManager urlManager;
        private List<ScreenScheme> historyList;
        private string appDomain;

        [SetUp]
        public void Binding()
        {
            var container = new DiContainer(StaticContext.Container);
            appDomain = "domain://";
            container.Bind<IUrlDomainProvider>().FromMethod(_ =>
            {
                var provider = Substitute.For<IUrlDomainProvider>();
                provider.Url.Returns(appDomain);
                return provider;
            });

            historyList = new List<ScreenScheme>();
            container.Bind<IHistory>()
                .FromMethod(_ =>
                {
                    var history = Substitute.For<IHistory>();
                    history.When(h => h.Add(Arg.Any<ScreenScheme>()))
                        .Do(callInfo => historyList.Add(callInfo.Arg<ScreenScheme>()));
                    return history;
                })
                .AsSingle();

            container.Bind<IUrlManager>()
                .To<UrlManager>()
                .AsSingle();

            container.Inject(this);
        }

        [Inject]
        public void Construct(IUrlManager urlManager)
        {
            this.urlManager = urlManager;
        }

        [Test]
        public void BuildToString_ScreenWithParameters_ReturnCombinedValidString()
        {
            var screenId = "MockScreenA";
            var parameters = new Dictionary<string, string> { ["test"] = "true", ["position"] = "10" };
            var expectedString = $"{appDomain}{screenId}?test=true&position=10";

            var result = urlManager.BuildToString(screenId, parameters);

            result.Should().Be(expectedString);
        }

        [Test]
        public void BuildToScheme_ScreenWithParameters_ReturnValidScheme()
        {
            var screenId = "MockScreenA";
            var parameters = new Dictionary<string, string> { ["test"] = "true", ["position"] = "10" };

            var result = urlManager.BuildToScheme(screenId, parameters);

            result.ScreenId.Should().Be(screenId);
            result.Parameters["test"].Should().Be("true");
            result.Parameters["position"].Should().Be("10");
        }

        [Test]
        public void Deserialize_DeepLink_ReturnValidScheme()
        {
            var screenId = "MockScreenA";
            var deepLink = $"{appDomain}{screenId}?test=true&position=10";

            var result = urlManager.Deserialize(deepLink);

            result.ScreenId.Should().Be(screenId);
            result.Parameters["test"].Should().Be("true");
            result.Parameters["position"].Should().Be("10");
        }

        [Test]
        public void Deserialize_DeepLinkWithHistory_AddStackToHistory()
        {
            var deepLink = "domain://MockScreenA?history=MockScreenB?position=10-MockScreenC?position=10&test=true&position=10";

            urlManager.Deserialize(deepLink);

            historyList.Count.Should().Be(2);
            historyList[0].ScreenId.Should().Be("MockScreenB");
            historyList[1].ScreenId.Should().Be("MockScreenC");
        }
    }
}