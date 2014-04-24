using System.Collections.Generic;
using System.Windows.Input;
using PowersOfTwo.Framework;
using PowersOfTwo.Services.Replay;

namespace PowersOfTwo.ViewModels
{
    public class ReplaySelectionViewModel : ObservableObject
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly OverlayViewModel _overlayViewModel;

        public ReplaySelectionViewModel(MainWindowViewModel mainWindowViewModel, OverlayViewModel overlayViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _overlayViewModel = overlayViewModel;
            WatchReplayCommand = new RelayCommand(p => WatchReplay(), p => SelectedReplay != null);
            LeaveCommand = new RelayCommand(p => _mainWindowViewModel.ShowMainMenu());
        }

        private void WatchReplay()
        {
            var replayPlayerViewModel = new ReplayPlayerViewModel(_mainWindowViewModel, _overlayViewModel);
            _mainWindowViewModel.Content = replayPlayerViewModel;
            replayPlayerViewModel.Initialize(SelectedReplay);
        }

        public async void Initialize()
        {
            _overlayViewModel.Show(new OverlayTextViewModel("Loading replays...", 32));
            var replayDiscoverer = new ReplayDiscoverer();
            AvailableReplays = new List<ReplayInformation>(await replayDiscoverer.GetReplayFiles());
            _overlayViewModel.Hide(null);
        }

        public List<ReplayInformation> AvailableReplays { get; private set; }

        public ReplayInformation SelectedReplay { get; set; }

        public ICommand WatchReplayCommand { get; private set; }

        public ICommand LeaveCommand { get; private set; }
    }
}
