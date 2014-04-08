using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Microsoft.AspNet.SignalR.Client;
using WebService;

namespace PowersOfTwo
{
    public class MainWindowViewModel : ViewModel
    {
        private GameLogic _gameLogic;

        private const int Rows = 4;
        private const int Columns = 4;

        private IHubProxy _gameProxy;
        private HubConnection _hubConnection;
        private StartGameInformation _gameInfo;
        private int _remainingPoints;

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

        public List<NumberCell> Cells { get { return _gameLogic.Cells; } }

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
            _gameInfo = startGameInformation;
            RemainingPoints = startGameInformation.StartPoints;

            _gameLogic = new GameLogic(Rows, Columns);
            _gameLogic.CellsMatched += _gameLogic_CellsMatched;
            _gameLogic.OutOfMoves += _gameLogic_OutOfMoves;
            OnPropertyChanged("Cells");
        }

        void _gameLogic_OutOfMoves()
        {
            _gameProxy.Invoke("OutOfMoves", _gameInfo.GroupName);
        }

        private void _gameLogic_CellsMatched(int points)
        {
            _gameProxy.Invoke("MatchTiles", _gameInfo.GroupName, points);
        }

        private void MoveLeft()
        {
            _gameLogic.MoveLeft();
        }

        private void MoveRight()
        {
            _gameLogic.MoveRight();
        }

        private void MoveUp()
        {
            _gameLogic.MoveUp();
        }

        private void MoveDown()
        {
            _gameLogic.MoveDown();
        }
    }
}
