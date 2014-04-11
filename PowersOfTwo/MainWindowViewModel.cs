namespace PowersOfTwo
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly MainMenuViewModel _mainMenuViewModel;

        public MainWindowViewModel()
        {
           _mainMenuViewModel = new MainMenuViewModel(this);
           ShowMainMenu();
        }

        public ViewModel Content { get; set; }

        public void ShowMainMenu()
        {
            Content = _mainMenuViewModel;
        }
    }
}