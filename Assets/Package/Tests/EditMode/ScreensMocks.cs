using Elselam.UnityRouter.Domain;
using System.Collections.Generic;
using UnityEngine;

namespace Elselam.UnityRouter.Tests
{

    public class OnExitInteractor : BaseScreenInteractor
    {
        public int OnExitCalled;

        public override void OnEnter()
        {
        }

        public override void WithParameters(IDictionary<string, string> parameters)
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

        public override void OnEnter()
        {
            OnEnterCalled++;
        }

        public override void WithParameters(IDictionary<string, string> parameters)
        {
            PageIndex = int.Parse(parameters["PageIndex"]);
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

        public override void WithParameters(IDictionary<string, string> parameters)
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
}