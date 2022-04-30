# Unity Router

Framework for Unity screens navigation with full support to deep link
You can control the navigation for your game or app using prefabs, scenes or both.

# Install

1 - In the Unity Package Manager window, add package from git url: \
"https://github.com/victor-elselam/unity-router.git?path=/Assets/Package#1.0.3" \
2 - Add this line to your manifest.json: \
"com.elselam.unity-router": "https://github.com/victor-elselam/unity-router.git?path=Assets/Package#1.0.3" \
3 - UPM - Work in progress, will be available soon

# Features
### Deep Link Support
When opening the app by a deep link, unity-router recognizes screen name and parameters in the link and open it. \
Deep link format should be: "{appDomain}/screenName?key1=value1&key2=value2&key3=value3". \
In order for it to work, you need to register your app domain following Unity documentation: \
Android: https://docs.unity3d.com/Manual/deep-linking-android.html \
iOS: https://docs.unity3d.com/2021.2/Documentation/Manual/deep-linking-ios.html
    

### View Transitions
unity-router can perform customized transitions by extending the ITransition interface in your Transition class, and coding the transition using whatever you want, like DOTween, or Unity API.

### Adapt it to your architecture
unity-router always deals with the 'IScreenPresenter' interface when navigating, including the access for Transforms to perform View Transitions. \
In our default setup, the 'IScreenFactory' used to perform the screen creation is the 'DefaultScreenFactory', which presumes that the IScreenPresenter is also the MonoBehaviour of the prefab. \
However, this is totally extensible by creating your own 'IScreenFactory' and make the relationships between them. For example, a MVP architecture Screen Factory:
 ```
 public class MvpScreenFactory : IScreenFactory
    {
        private DiContainer container;

        public MvpScreenFactory(DiContainer container)
        {
            this.container = container;
        }

        public IScreenModel Create(IScreenRegistry screenRegistry)
        {
            var instance = container.InstantiatePrefabForComponent<IBaseView>(screenRegistry.ScreenPrefab);
            instance.Disable();
            BindView(instance);

            var presenter = (IScreenPresenter) container.ResolveId(screenRegistry.ScreenPresenter, screenRegistry.ScreenId);
            return new ScreenModel(screenRegistry.ScreenId, presenter);
        }

        private void BindView(IBaseView instance) => 
            container.BindInterfacesTo(instance.GetType()).FromInstance(instance).AsSingle().IfNotBound();
    }
 ```
 
### Parameters Management
by using the 'IParameterManager' you can easily create and parse dynamic parameters (or payloads) sent between screens, it has full support to structures and complex objects, that is passed by serializing/deserializing

### Dependency Injection
this framework was built with Zenject on premise, but you can use with it or not, it's up to you! \
There're examples on how to use both at this Repository, at Assets/Samples

# Get Started (Dependency Injection)

1 - Install the package \
2 - Create a instance of Navigation Installer, or a new class using your own bindings and add it to your Context (Create/Elselam/UnityRouter/Installers/Navigation) \
![image](https://user-images.githubusercontent.com/62479476/166111254-d64d0a41-8c4c-43ad-b9d1-0c6a2ea1f960.png)
3 - Create a new ScreensInstaller (Create/Elselam/UnityRouter/Installers/Screen) also add it to your Context \
4 - Create a new class inheriting from BaseAreaInstaller and in the GetScreens method, create your screens registries. Example:
 ```
 [CreateAssetMenu(fileName = "MainMenuScreensInstaller", menuName = "Project/Installers/MainMenuInstaller", order = 0)]
    public class RealScratchScreensInstaller : BaseScreensInstaller
    {
        [SerializeField] private GameObject homeScreenPrefab;
        [SerializeField] private GameObject settingsScreenPrefab;

        public override List<IScreenRegistry> GetScreens()
        {
            var list = new List<IScreenRegistry>();
            list.Add(new ScreenRegistry("HomeScreen", typeof(HomeScreenPresenter), homeScreenPrefab));
            list.Add(new ScreenRegistry("SettingsScreen", typeof(SettingsScreenPresenter), settingsScreenPrefab));

            return list;
        }
    }
```
5 - Create an instance of it and add to the ScreensInstaller inspector (AreasInstaller). This is meant to separate screens in small installers pieces. \
![image](https://user-images.githubusercontent.com/62479476/166112645-5524d0f0-8f54-40f2-82d0-02ac192f9b54.png)

6 - In your application startup, receive the INavigation from DI and when your application is ready, call 
 ```
navigation.Initialize();
navigation.NavigateToDefaultScreen(); //or to any other of your preference by navigation.NavigateTo<{ScreenPresenterName}>()
 ```

# Get Started (Facade)
1 - Install the package \
2 - When your application is ready, call:
 ```
UnityRouter.Setup(settings, GetScreens());
UnityRouter.Create();
UnityRouter.Navigation.NavigateToDefaultScreen();
```

# Samples

### Sample 1 - Using with Dependency Injection
ChangeSceneSample - To Test it, just open the 'ChangeSceneSample' scene, hit play, and navigate through the different screens/scenes
### Sample 2 - Using without Dependency Injection
UsageWithoutDependencyInjection - To Test it, just open the 'UsageWithoutDependencyInjection' scene, hit play, and navigate through the different screens/scenes.

In both of them, maybe you'll need to add the scenes to the Build Settings. For some reason, Unity keeps changing the AssetID and the reference goes away.

# Expanding

- Support for deep link in the middle of a session (high priority)
- Support for subflows in History Service
- Support to use 'On Demand' (Create/Destroy screens when Enter/Exit)
- Refactor 'Navigation' names to 'UnityRouter' - DONE
- Add support for an extern logger to give more control of those logs for your system
- Add event when a specific parameter is found in deep links/navigations (with this, we can remove History methods from UrlManager, also, give a lot of flexibility for the framework)
- Create a Scene dragger to the inspector, to avoid missing reference when change scene name
- Create an adapter for Unity DeepLink receiver, avoiding our classes to be coupled with it.

- The entire framework is Tested with Unit Tests that runs on CI when a PR is opened.
![image](https://user-images.githubusercontent.com/62479476/166111942-7ebac27d-4837-46fd-95ab-11bcd974bc53.png)

# Dependencies

"com.boundfoxstudios.fluentassertions": "https://github.com/BoundfoxStudios/fluentassertions-unity.git#upm", \
"com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask", \
"com.svermeulen.extenject": "https://github.com/starikcetin/Extenject.git#9.1.0", \
"com.unity.nuget.newtonsoft-json": "3.0.2"
