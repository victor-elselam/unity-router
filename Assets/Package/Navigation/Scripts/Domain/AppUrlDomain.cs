using elselam.Navigation.Domain;
using UnityEngine;

namespace elselam.Navigation.Domain {
    [CreateAssetMenu(fileName = "UrlDomain", menuName = "Elselam/UNavScreen/UrlDomain")]
    public class AppUrlDomain : ScriptableObject, IUrlDomainProvider {
        [SerializeField] private string url = "domain://";
        public string Url => url;
    }
}