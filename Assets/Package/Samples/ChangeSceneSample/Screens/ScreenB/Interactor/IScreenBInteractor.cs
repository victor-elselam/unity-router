namespace Sample.ChangeSceneSample.Screens.ScreenB.Interactor
{
    public interface IScreenBInteractor
    {
        void LoadScene(string scene);
        void UpdateElementPosition(float position);
        void BackToLastScreen();
    }
}