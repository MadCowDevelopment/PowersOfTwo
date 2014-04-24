﻿using System;
using System.Windows;
using System.Windows.Input;

using Microsoft.AspNet.SignalR.Client;

using PowersOfTwo.Dto;
using PowersOfTwo.Framework;
using PowersOfTwo.Services;

namespace PowersOfTwo.ViewModels
{
    public class MainMenuViewModel : ObservableObject, IDisposable
    {
        #region Fields

        private readonly GameProxy _gameProxy;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly OverlayViewModel _overlayViewModel;

        #endregion Fields

        #region Constructors

        public MainMenuViewModel(MainWindowViewModel mainWindowViewModel, OverlayViewModel overlayViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _overlayViewModel = overlayViewModel;

            _gameProxy = new GameProxy();
            _gameProxy.ConnectionStateChanged += change =>
            {
                CanStartDuel = change.NewState == ConnectionState.Connected;
                CanObserveGame = change.NewState == ConnectionState.Connected;
            };

            PlayDuelCommand = new RelayCommand(p => SelectDuelGameMode());
            PlaySoloCommand = new RelayCommand(p => StartSoloGame());
            ObserveGameCommand = new RelayCommand(p => ObserveGame());
            QuitCommand = new RelayCommand(p => Application.Current.Shutdown());
            ReplayCommand = new RelayCommand(p => Replay());
        }

        #endregion Constructors

        #region Public Properties

        public bool CanObserveGame
        {
            get; private set;
        }

        public bool CanStartDuel
        {
            get;
            private set;
        }

        public ICommand ObserveGameCommand
        {
            get; private set;
        }

        public ICommand PlayDuelCommand
        {
            get;
            private set;
        }

        public ICommand PlaySoloCommand
        {
            get;
            private set;
        }

        public ICommand QuitCommand
        {
            get;
            private set;
        }

        public ICommand ReplayCommand
        {
            get; private set;
        }

        #endregion Public Properties

        #region Public Methods

        public void Dispose()
        {
            _gameProxy.Dispose();
        }

        #endregion Public Methods

        #region Private Methods

        private void ObserveGame()
        {
            var runningGamesViewModel = new RunningGamesViewModel(_mainWindowViewModel, _overlayViewModel, _gameProxy);
            _mainWindowViewModel.Content = runningGamesViewModel;
            runningGamesViewModel.Initialize();
        }

        private void Replay()
        {
            var replayViewModel = new ReplaySelectionViewModel(_mainWindowViewModel, _overlayViewModel);
            _mainWindowViewModel.Content = replayViewModel;
            replayViewModel.Initialize();
        }

        private void SelectDuelGameMode()
        {
            _mainWindowViewModel.Content = new GameModeSelectionViewModel(_mainWindowViewModel, _overlayViewModel,
                _gameProxy);
        }

        private void StartSoloGame()
        {
            _mainWindowViewModel.Content = new SoloPlayViewModel(_overlayViewModel, _mainWindowViewModel);
        }

        #endregion Private Methods
    }
}