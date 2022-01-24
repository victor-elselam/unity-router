using Elselam.UnityRouter.Installers;
using UnityEngine;
using UnityEngine.UI;

namespace Sample.UsageWithoutDependencyInjection.Scripts
{
    public class BackToScene : MonoBehaviour
    {
        public void Start()
        {
            GetComponent<Button>().onClick.AddListener(
                () => UnityRouter.Navigation.BackToLastScreen());
        }
    }
}
