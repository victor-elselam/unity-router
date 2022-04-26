using Elselam.UnityRouter.Domain;

namespace Elselam.UnityRouter.Installers
{
    public class ScreenModel : IScreenModel
    {
        public string ScreenId { get; }
        public IScreenPresenter Presenter { get; }

        public ScreenModel(string screenId, IScreenPresenter presenter)
        {
            ScreenId = screenId;
            Presenter = presenter;
        }
    }
}