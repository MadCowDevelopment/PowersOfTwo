using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PowersOfTwo.Dto;
using PowersOfTwo.Framework;
using PowersOfTwo.Services;

namespace PowersOfTwo.ViewModels
{
    public class RunningGamesViewModel : ObservableObject
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly OverlayViewModel _overlayViewModel;
        private readonly GameProxy _gameProxy;

        public RunningGamesViewModel(MainWindowViewModel mainWindowViewModel, OverlayViewModel overlayViewModel, GameProxy gameProxy)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _overlayViewModel = overlayViewModel;
            _gameProxy = gameProxy;

            JoinGameCommand = new RelayCommand(p => JoinGame(), p => SelectedGame != null);
            LeaveCommand = new RelayCommand(p => Leave());
        }

        private void Leave()
        {
            _mainWindowViewModel.ShowMainMenu();
        }

        private async void JoinGame()
        {
            _gameProxy.JoinAsSpectator(SelectedGame.GroupName, Environment.MachineName);
            var observeGameViewModel = new ObserveGameViewModel(_mainWindowViewModel, _gameProxy, _overlayViewModel);
            _overlayViewModel.Show(new OverlayTextViewModel("Joining game...", 36), allowAccept: false);
            await observeGameViewModel.Initialize(SelectedGame.GroupName);
            _overlayViewModel.Hide(null);
            _mainWindowViewModel.Content = observeGameViewModel;
        }

        public async void Initialize()
        {
            _overlayViewModel.Show(new OverlayTextViewModel("Getting games...", 36), allowAccept: false);
            var runningGames = await _gameProxy.GetRunningGames();
            RunningGames = new ObservableCollection<RunningGameDto>(runningGames);
            _overlayViewModel.Hide(null);
        }

        public ICommand JoinGameCommand { get; private set; }
        public ICommand LeaveCommand { get; private set; }
        public RunningGameDto SelectedGame { get; set; }

        public ObservableCollection<RunningGameDto> RunningGames { get; private set; }
    }
}