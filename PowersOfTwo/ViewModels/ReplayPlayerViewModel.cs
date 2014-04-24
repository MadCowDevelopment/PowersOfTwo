using System.Windows.Input;
using PowersOfTwo.Framework;
using PowersOfTwo.Services.Replay;

namespace PowersOfTwo.ViewModels
{
    public class ReplayPlayerViewModel : ObservableObject, IReplayTarget
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly OverlayViewModel _overlayViewModel;

        public ReplayPlayerViewModel(MainWindowViewModel mainWindowViewModel, OverlayViewModel overlayViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _overlayViewModel = overlayViewModel;

            PlayCommand = new RelayCommand(p => Play());
            PauseCommand = new RelayCommand(p => Pause());
            StopCommand = new RelayCommand(p => Stop());
            IncreasSpeedCommand = new RelayCommand(p => IncreaseSpeed());
            DecreaseSpeedCommand = new RelayCommand(p => DecreaseSpeed());
            LeaveCommand = new RelayCommand(p => Leave());
        }

        private void Play()
        {
            Player.Play(this);
            IsPlaying = true;
        }

        public bool IsPlaying { get; private set; }

        private void Pause()
        {
            Player.Pause();
            IsPlaying = false;
        }

        private void Stop()
        {
            Player.Stop();
            IsPlaying = false;
        }

        private void IncreaseSpeed()
        {
            Player.IncreaseSpeed();
        }

        private void DecreaseSpeed()
        {
            Player.DecreaseSpeed();
        }

        private void Leave()
        {
            IsPlaying = false;
            _mainWindowViewModel.ShowMainMenu();
        }

        public async void Initialize(ReplayInformation replayInformation)
        {
            _overlayViewModel.Show(new OverlayTextViewModel("Loading replay...", 32));
            var replayData = await new ReplayLoader().Load(replayInformation.Fullpath);
            _overlayViewModel.Hide(null);
            Player = new ReplayPlayer(replayData);
        }

        public ReplayPlayer Player { get; private set; }

        public ICommand PlayCommand { get; private set; }
        public ICommand PauseCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        public ICommand LeaveCommand { get; private set; }

        public ICommand IncreasSpeedCommand { get; private set; }

        public ICommand DecreaseSpeedCommand { get; private set; }
        public PlayerViewModel Player1 { get; set; }
        public PlayerViewModel Player2 { get; set; }
    }
}
