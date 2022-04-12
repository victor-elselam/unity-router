using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.Installers;
using JetBrains.Annotations;
using NSubstitute;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Elselam.UnityRouter.Tests.Mocks
{
    public class OnExitPresenter : IScreenPresenter
    {
        public Transform Transform { get; }

        public void Enable()
        {
        }

        public void Disable()
        {
        }

        public void OnEnter([CanBeNull] IDictionary<string, string> parameters) { }
        public IDictionary<string, string> OnExit() { return null; }
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

        public void OnEnter([CanBeNull] IDictionary<string, string> parameters) { }
        public IDictionary<string, string> OnExit() { return null; }
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

        public void OnEnter([CanBeNull] IDictionary<string, string> parameters) { }
        public IDictionary<string, string> OnExit() { return null; }
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

        public void OnEnter([CanBeNull] IDictionary<string, string> parameters) { }
        public IDictionary<string, string> OnExit() { return null; }
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

        public void OnEnter([CanBeNull] IDictionary<string, string> parameters) { }
        public IDictionary<string, string> OnExit() { return null; }
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

        public void OnEnter([CanBeNull] IDictionary<string, string> parameters) { }
        public IDictionary<string, string> OnExit() { return null; }
    }

    public class UnregisteredScreenPresenter : IScreenPresenter
    {
        public Transform Transform { get; }

        public void Enable()
        {
        }

        public void Disable()
        {
        }

        public void OnEnter([CanBeNull] IDictionary<string, string> parameters) { }
        public IDictionary<string, string> OnExit() { return null; }
    }

    public class ScreenMocks
    {
        public static void RegisterScreensModels(DiContainer container)
        {
            var registryA = Substitute.For<IScreenRegistry>();
            registryA.ScreenPresenter.Returns(typeof(ScreenAPresenter));
            registryA.ScreenId.Returns("MockScreenA");
            container.Bind<IScreenRegistry>().WithId("MockScreenA").FromInstance(registryA);
            container.Bind<ScreenAPresenter>()
                .AsSingle();
            container.Bind<IScreenModel>().WithId("MockScreenA").FromMethod(_ =>
                new ScreenModel("MockScreenA", container.Resolve<ScreenAPresenter>()));

            var registryB = Substitute.For<IScreenRegistry>();
            registryB.ScreenPresenter.Returns(typeof(ScreenBPresenter));
            registryB.ScreenId.Returns("MockScreenB");
            container.Bind<IScreenRegistry>().WithId("MockScreenB").FromInstance(registryB);
            container.Bind<ScreenBPresenter>()
                .AsSingle();
            container.Bind<IScreenModel>().WithId("MockScreenB").FromMethod(_ =>
                new ScreenModel("MockScreenB", container.Resolve<ScreenBPresenter>()));

            var registryCustom = Substitute.For<IScreenRegistry>();
            registryCustom.ScreenPresenter.Returns(typeof(CustomScreenPresenter));
            registryCustom.ScreenId.Returns("MockScreenCustom");
            container.Bind<IScreenRegistry>().WithId("MockScreenCustom").FromInstance(registryCustom);
            container.Bind<CustomScreenPresenter>()
                .AsSingle();
            container.Bind<IScreenModel>().WithId("MockScreenCustom").FromMethod(_ =>
                new ScreenModel("MockScreenCustom", container.Resolve<CustomScreenPresenter>()));

            var emptyIdRegistry = Substitute.For<IScreenRegistry>();
            emptyIdRegistry.ScreenPresenter.Returns(typeof(EmptyIdPresenter));
            emptyIdRegistry.ScreenId.Returns(string.Empty);
            container.Bind<IScreenRegistry>().WithId("EmptyIdScreenCustom").FromInstance(emptyIdRegistry);
            container.Bind<EmptyIdPresenter>()
                .AsSingle();
            container.Bind<IScreenModel>().WithId(string.Empty).FromMethod(_ =>
                new ScreenModel("EmptyIdScreenCustom", container.Resolve<EmptyIdPresenter>()));

            var sameIdRegistry = Substitute.For<IScreenRegistry>();
            sameIdRegistry.ScreenPresenter.Returns(typeof(SameScreenPresenter));
            sameIdRegistry.ScreenId.Returns("MockSameScreen");
            container.Bind<IScreenRegistry>().WithId("MockSameScreen").FromInstance(sameIdRegistry);
            container.Bind<SameScreenPresenter>()
                .AsSingle();
            container.Bind<IScreenModel>().WithId("MockSameScreen").FromMethod(_ =>
                new ScreenModel("MockSameScreen", container.Resolve<SameScreenPresenter>()));

            var onExitRegistry = Substitute.For<IScreenRegistry>();
            onExitRegistry.ScreenPresenter.Returns(typeof(OnExitPresenter));
            onExitRegistry.ScreenId.Returns("MockExitScreen");
            container.Bind<IScreenRegistry>().WithId("MockExitScreen").FromInstance(onExitRegistry);
            container.Bind<OnExitPresenter>()
                     .AsSingle();
            container.Bind<IScreenModel>().WithId("MockExitScreen").FromMethod(_ =>
                new ScreenModel("MockExitScreen", container.Resolve<OnExitPresenter>()));

            var registries = new List<IScreenRegistry> { registryA, registryB, registryCustom, emptyIdRegistry, sameIdRegistry, onExitRegistry };
            container.Bind<List<IScreenRegistry>>()
                .FromInstance(registries)
                .AsSingle();
        }
    }
}