using PowersOfTwo.Framework;

namespace PowersOfTwo.ViewModels
{
    public class ReplayPlayerViewModel : ObservableObject
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        public ReplayPlayerViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public async void Initialize()
        {
        }
    }
}
