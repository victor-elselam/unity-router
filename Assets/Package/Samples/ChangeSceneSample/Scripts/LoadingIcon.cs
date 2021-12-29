using UnityEngine;

namespace Sample.ChangeSceneSample.Scripts
{
    public class LoadingIcon : MonoBehaviour
    {

        private void Update()
        {
            transform.eulerAngles += new Vector3(0, 0, 5);
        }
    }
}