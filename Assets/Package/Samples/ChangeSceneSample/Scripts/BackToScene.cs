using Elselam.UnityRouter.Installers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sample.ChangeSceneSample.Scripts
{
    public class BackToScene : MonoBehaviour
    {
        [Inject]
        public void Construct(INavigation navigation)
        {
            GetComponent<Button>().onClick.AddListener(
                () => navigation.BackToLastScreen());
        }
    }
}
