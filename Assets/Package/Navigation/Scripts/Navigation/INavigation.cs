using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.History;
using Elselam.UnityRouter.Transitions;
using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Elselam.UnityRouter.Installers
{
    /// <summary>
    /// Main interface, used to perform all navigations and transitions
    /// </summary>
    public interface INavigation
    {
        /// <summary>
        /// Initialize Service
        /// </summary>
        void Initialize();

        /// <summary>
        /// Load Default Screen. specified in 'NavigationInstaller' in the field 'DefaultScreen'
        /// </summary>
        void NavigateToDefaultScreen();

        /// <summary>
        /// Navigate to specified type, the target is required to be a Interactor screen, aiming to follow clean architecture.
        /// </summary>
        /// <typeparam name="TScreen">Target screen interactor type</typeparam>
        /// <param name="transition">Instance of the desired target, if null, system will select default transition</param>
        /// <param name="parameters">Parameters to send to the target screen</param>
        void NavigateTo<TScreen>([CanBeNull] ITransition transition = null, IDictionary<string, string> parameters = null) where TScreen : IScreenInteractor;

        /// <summary>
        /// Variant to allow targeting by dynamic types instead of generic.
        /// </summary>
        /// <param name="screenType">Target screen interactor type</param>
        /// <param name="transition">Instance of the desired target, if null, system will select default transition</param>
        /// <param name="parameters">Parameters to send to the target screen</param>
        void NavigateTo(Type screenType, [CanBeNull] ITransition transition = null, IDictionary<string, string> parameters = null);

        /// <summary>
        /// Variant to allow targeting direct a ScreenScheme
        /// </summary>
        /// <param name="enterScheme">Target screen scheme</param>
        /// <param name="transition">Instance of the desired target, if null, system will select default transition</param>
        void NavigateTo(ScreenScheme enterScheme, [CanBeNull] ITransition transition = null);

        /// <summary>
        /// Variant to allow targeting by name instead of types.
        /// </summary>
        /// <param name="screenType">Target screen interactor type</param>
        /// <param name="transition">Instance of the desired target, if null, system will select default transition</param>
        /// <param name="parameters">Parameters to send to the target screen</param>
        void NavigateTo(string screenName, ITransition transition = null, IDictionary<string, string> parameters = null);

        /// <summary>
        /// Back to the previous screen
        /// </summary>
        /// <param name="transition">Instance of the desired target, if null, system will select default transition</param>
        void BackToLastScreen([CanBeNull] ITransition transition = null);
    }
}