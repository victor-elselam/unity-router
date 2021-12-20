using Sample.ChangeSceneSample.Scripts;
using UnityEngine;
using Zenject;

namespace Package.Samples.ChangeSceneSample
{
    [CreateAssetMenu(fileName = "AppStartInstaller", menuName = "Elselam/UNavScreen/Installers/AppStartInstaller")]
    public class AppStartInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ChangeSceneAppStart>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .NonLazy();
        }
    }
}
