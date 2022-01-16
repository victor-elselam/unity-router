using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.Installers;
using NSubstitute;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Elselam.UnityRouter.Tests.Mocks
{
    public class OnExitInteractor : BaseScreenInteractor
    {
        public int OnExitCalled;

        public override void OnEnter(IDictionary<string, string> parameters)
        {
        }

        public override IDictionary<string, string> OnExit()
        {
            OnExitCalled++;
            return base.OnExit();
        }
    }

    public class OnExitPresenter : IScreenPresenter
    {
        public Transform Transform { get; }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }

    public class SameScreenInteractor : BaseScreenInteractor
    {
        public int PageIndex = 0;
        public int OnEnterCalled = 0;

        public override void OnEnter(IDictionary<string, string> parameters)
        {
            PageIndex = int.Parse(parameters["PageIndex"]);
            OnEnterCalled++;
        }

        public override IDictionary<string, string> OnExit()
        {
            return new Dictionary<string, string> {
                {"PageIndex", PageIndex.ToString()},
            };
        }
    }

    public class SameScreenPresenter : IScreenPresenter
    {
        public Transform Transform { get; }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }

    /// <summary>
    /// Simulate custom screen created by app
    /// </summary>
    public class CustomScreenInteractor : BaseScreenInteractor
    {
        public float ItemLength = 0;
        public Vector3 ItemPosition = Vector3.zero;

        public override void OnEnter(IDictionary<string, string> parameters)
        {
            ItemLength = float.Parse(parameters["ItemLength"]);
            ItemPosition = JsonUtility.FromJson<Vector3>(parameters["ItemPosition"]);
        }

        public override IDictionary<string, string> OnExit()
        {
            return new Dictionary<string, string> {
                {"ItemLength", "15"},
                {"ItemPosition", JsonUtility.ToJson(new Vector3(15f, 10f, 5f))},
            };
        }
    }

    public class CustomScreenPresenter : IScreenPresenter
    {
        public Transform Transform { get; }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }

    public class UnregisteredScreenInteractor : BaseScreenInteractor
    {
    }

    public class ScreenAInteractor : BaseScreenInteractor
    {
    }

    public class ScreenAPresenter : IScreenPresenter
    {
        public Transform Transform { get; }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }

    public class ScreenBInteractor : BaseScreenInteractor
    {
    }

    public class ScreenBPresenter : IScreenPresenter
    {
        public Transform Transform { get; }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }

    public class EmptyIdInteractor : BaseScreenInteractor
    {
    }

    public class EmptyIdPresenter : IScreenPresenter
    {
        public Transform Transform { get; }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }

    public class ScreenMocks
    {
        public static void RegisterScreensModels(DiContainer container)
        {
            var registryA = Substitute.For<IScreenRegistry>();
            registryA.ScreenInteractor.Returns(typeof(ScreenAInteractor));
            registryA.ScreenPresenter.Returns(typeof(ScreenAPresenter));
            registryA.ScreenId.Returns("MockScreenA");
            container.Bind<IScreenRegistry>().WithId("MockScreenA").FromInstance(registryA);
            container.Bind<ScreenAInteractor>()
                .AsSingle();
            container.Bind<ScreenAPresenter>()
                .AsSingle();
            container.Bind<IScreenModel>().WithId("MockScreenA").FromMethod(_ =>
                new ScreenModel("MockScreenA",
                    container.Resolve<ScreenAInteractor>(),
                    container.Resolve<ScreenAPresenter>(),
                    null));

            var registryB = Substitute.For<IScreenRegistry>();
            registryB.ScreenInteractor.Returns(typeof(ScreenBInteractor));
            registryB.ScreenPresenter.Returns(typeof(ScreenBPresenter));
            registryB.ScreenId.Returns("MockScreenB");
            container.Bind<IScreenRegistry>().WithId("MockScreenB").FromInstance(registryB);
            container.Bind<ScreenBInteractor>()
                .AsSingle();
            container.Bind<ScreenBPresenter>()
                .AsSingle();
            container.Bind<IScreenModel>().WithId("MockScreenB").FromMethod(_ =>
                new ScreenModel("MockScreenB",
                    container.Resolve<ScreenBInteractor>(),
                    container.Resolve<ScreenBPresenter>(),
                    null));

            var registryCustom = Substitute.For<IScreenRegistry>();
            registryCustom.ScreenInteractor.Returns(typeof(CustomScreenInteractor));
            registryCustom.ScreenPresenter.Returns(typeof(CustomScreenPresenter));
            registryCustom.ScreenId.Returns("MockScreenCustom");
            container.Bind<IScreenRegistry>().WithId("MockScreenCustom").FromInstance(registryCustom);
            container.Bind<CustomScreenInteractor>()
                .AsSingle();
            container.Bind<CustomScreenPresenter>()
                .AsSingle();
            container.Bind<IScreenModel>().WithId("MockScreenCustom").FromMethod(_ =>
                new ScreenModel("MockScreenCustom",
                    container.Resolve<CustomScreenInteractor>(),
                    container.Resolve<CustomScreenPresenter>(),
                    null));

            var emptyIdRegistry = Substitute.For<IScreenRegistry>();
            emptyIdRegistry.ScreenInteractor.Returns(typeof(EmptyIdInteractor));
            emptyIdRegistry.ScreenPresenter.Returns(typeof(EmptyIdPresenter));
            emptyIdRegistry.ScreenId.Returns(string.Empty);
            container.Bind<IScreenRegistry>().WithId("EmptyIdScreenCustom").FromInstance(emptyIdRegistry);
            container.Bind<EmptyIdInteractor>()
                .AsSingle();
            container.Bind<EmptyIdPresenter>()
                .AsSingle();
            container.Bind<IScreenModel>().WithId(string.Empty).FromMethod(_ =>
                new ScreenModel("EmptyIdScreenCustom",
                    container.Resolve<EmptyIdInteractor>(),
                    container.Resolve<EmptyIdPresenter>(),
                    null));

            var sameIdRegistry = Substitute.For<IScreenRegistry>();
            sameIdRegistry.ScreenInteractor.Returns(typeof(SameScreenInteractor));
            sameIdRegistry.ScreenPresenter.Returns(typeof(SameScreenPresenter));
            sameIdRegistry.ScreenId.Returns("MockSameScreen");
            container.Bind<IScreenRegistry>().WithId("MockSameScreen").FromInstance(sameIdRegistry);
            container.Bind<SameScreenInteractor>()
                .AsSingle();
            container.Bind<SameScreenPresenter>()
                .AsSingle();
            container.Bind<IScreenModel>().WithId("MockSameScreen").FromMethod(_ =>
                new ScreenModel("MockSameScreen",
                    container.Resolve<SameScreenInteractor>(),
                    container.Resolve<SameScreenPresenter>(),
                    null));

            var onExitRegistry = Substitute.For<IScreenRegistry>();
            onExitRegistry.ScreenInteractor.Returns(typeof(OnExitInteractor));
            onExitRegistry.ScreenPresenter.Returns(typeof(OnExitPresenter));
            onExitRegistry.ScreenId.Returns("MockExitScreen");
            container.Bind<IScreenRegistry>().WithId("MockExitScreen").FromInstance(onExitRegistry);
            container.Bind<OnExitInteractor>()
                     .AsSingle();
            container.Bind<OnExitPresenter>()
                     .AsSingle();
            container.Bind<IScreenModel>().WithId("MockExitScreen").FromMethod(_ =>
                new ScreenModel("MockExitScreen",
                    container.Resolve<OnExitInteractor>(),
                    container.Resolve<OnExitPresenter>(),
                    null));

            var registries = new List<IScreenRegistry> { registryA, registryB, registryCustom, emptyIdRegistry, sameIdRegistry, onExitRegistry };
            container.Bind<List<IScreenRegistry>>()
                .FromInstance(registries)
                .AsSingle();
        }
    }
}