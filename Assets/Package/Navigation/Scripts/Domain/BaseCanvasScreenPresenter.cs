using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

namespace Elselam.UnityRouter.Domain
{
    /// <summary>
    /// This class should be used when implementing an UI screen
    /// </summary>
    public abstract class BaseCanvasScreenPresenter : BaseScreenPresenter
    {
        [SerializeField] private Transform screenContainer;
        public override Transform Transform => screenContainer;
    }
}