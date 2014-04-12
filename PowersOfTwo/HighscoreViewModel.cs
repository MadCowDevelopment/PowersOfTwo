using System.Windows.Input;

namespace PowersOfTwo
{
    public class HighscoreViewModel : ViewModel
    {
        public HighscoreViewModel(MainWindowViewModel mainWindowViewModel)
        {
            BackToMainMenuCommand = new RelayCommand(p => mainWindowViewModel.ShowMainMenu());
        }

        public ICommand BackToMainMenuCommand { get; private set; }
    }
}
