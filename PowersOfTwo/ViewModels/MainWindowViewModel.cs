using PowersOfTwo.Framework;
using PowersOfTwo.Services;

namespace PowersOfTwo.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly MainMenuViewModel _mainMenuViewModel;

        public MainWindowViewModel()
        {
            Overlay = new OverlayViewModel();

            _mainMenuViewModel = new MainMenuViewModel(this);
            CommandBroker = new GlobalCommandBroker(this);
            ShowMainMenu();
        }

        public GlobalCommandBroker CommandBroker { get; private set; }

        public ObservableObject Content { get; set; }

        public OverlayViewModel Overlay { get; private set; }

        public void ShowMainMenu()
        {
            Content = _mainMenuViewModel;
        }

        public void ShowHighscore()
        {
            Content = new HighscoreViewModel(this);
        }
    }
}