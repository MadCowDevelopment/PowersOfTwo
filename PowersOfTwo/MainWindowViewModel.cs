using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Microsoft.AspNet.SignalR.Client;
using PowersOfTwo.Core;
using WebService;

namespace PowersOfTwo
{
    public class MainWindowViewModel : ViewModel
    {
        private IHubProxy _gameProxy;
        private HubConnection _hubConnection;
        private int _remainingPoints;
        private List<NumberCell> _cells;

        public MainWindowViewModel()
        {
            Initialize();
            LeftCommand = new RelayCommand(p => MoveLeft());
            RightCommand = new RelayCommand(p => MoveRight());
            UpCommand = new RelayCommand(p => MoveUp());
            DownCommand = new RelayCommand(p => MoveDown());
        }

        public ICommand LeftCommand { get; private set; }

        public ICommand RightCommand { get; private set; }

        public ICommand UpCommand { get; private set; }

        public ICommand DownCommand { get; private set; }

        public List<NumberCell> Cells
        {
            get { return _cells; }
            private set { _cells = value; OnPropertyChanged(); }
        }

        private void Initialize()
        {
            //_hubConnection = new HubConnection("http://localhost:8369");
            //_hubConnection = new HubConnection("http://powersoftwo.apphb.com");
            _hubConnection = new HubConnection("http://pc-mgr-2:8369");

            _hubConnection.TraceLevel = TraceLevels.All;
            _hubConnection.TraceWriter = Console.Out;

            _gameProxy = _hubConnection.CreateHubProxy("GameHub");
            _gameProxy.On<StartGameInformation>("StartGame", GameStarted);
            _gameProxy.On<bool>("GameOver", GameOver);
            _gameProxy.On<int>("UpdatePoints", UpdatePoints);

            _hubConnection.Start().Wait();

            _gameProxy.Invoke("Queue", "TEST");
        }

        private void UpdatePoints(int remainingPoints)
        {
            RemainingPoints = remainingPoints;
        }

        public int RemainingPoints
        {
            get { return _remainingPoints; }
            set
            {
                _remainingPoints = value; OnPropertyChanged();
            }
        }

        private void GameOver(bool win)
        {
            MessageBox.Show(win ? "You win." : "You lose.");
            Application.Current.Shutdown();
        }

        private void GameStarted(StartGameInformation startGameInformation)
        {
            RemainingPoints = startGameInformation.StartPoints;
            GroupName = startGameInformation.GroupName;
            Cells = startGameInformation.Cells;
        }

        private string GroupName { get; set; }

        private void MoveLeft()
        {
            Cells = _gameProxy.Invoke<List<NumberCell>>("MoveLeft", GroupName).Result;
        }

        private void MoveRight()
        {
            Cells = _gameProxy.Invoke<List<NumberCell>>("MoveRight", GroupName).Result;
        }

        private void MoveUp()
        {
            Cells = _gameProxy.Invoke<List<NumberCell>>("MoveUp", GroupName).Result;
        }

        private void MoveDown()
        {
            Cells = _gameProxy.Invoke<List<NumberCell>>("MoveDown", GroupName).Result;
        }
    }
}
