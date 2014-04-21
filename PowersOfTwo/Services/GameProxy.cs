using System;
using System.Collections.Generic;

using Microsoft.AspNet.SignalR.Client;

using PowersOfTwo.Core;

using WebService;

namespace PowersOfTwo.Services
{
    public class GameProxy
    {
        #region Fields

        private readonly IHubProxy _gameProxy;
        private readonly HubConnection _hubConnection;

        #endregion Fields

        #region Constructors

        public GameProxy()
        {
            //_hubConnection = new HubConnection("http://localhost:8369");
            //_hubConnection = new HubConnection("http://4096.azurewebsites.net/");
            //_hubConnection = new HubConnection("http://powersoftwo.apphb.com");
            _hubConnection = new HubConnection("http://pc-mgr-2:8369");

            _hubConnection.TraceLevel = TraceLevels.All;
            _hubConnection.TraceWriter = Console.Out;

            _hubConnection.StateChanged += HubConnectionStateChanged;

            _gameProxy = _hubConnection.CreateHubProxy("GameHub");
            _gameProxy.On<StartGameInformation>("StartGame", OnGameStarted);
            _gameProxy.On<bool>("GameOver", RaiseGameOver);
            _gameProxy.On<int>("UpdatePoints", RaisePointsUpdated);
            _gameProxy.On<List<NumberCell>>("CellsChanged", RaiseCellsChanged);
            _gameProxy.On<List<NumberCell>>("OpponentCellsChanged", RaiseOpponentCellsChanged);
            _gameProxy.On<int>("UpdateOpponentPoints", RaiseOpponentPointsUpdated);
            _gameProxy.On("OpponentLeft", RaiseOpponentLeft);
            _gameProxy.On("GameCanceled", RaiseGameCanceled);
            _gameProxy.On<OpponentFoundInformation>("OpponentFound", OnOpponentFound);
            _gameProxy.On("OpponentReady", RaiseOpponentReady);
            _gameProxy.On<int>("QueueRemainingTimeChanged", RaiseQueueRemainingTimeChanged);

            _hubConnection.Start();
        }

        #endregion Constructors

        #region Events

        public event Action<List<NumberCell>> CellsChanged;

        public event Action<StateChange> ConnectionStateChanged;

        public event Action GameCanceled;

        public event Action<bool> GameOver;

        public event Action<StartGameInformation> GameStarted;

        public event Action<List<NumberCell>> OpponentCellsChanged;

        public event Action<string> OpponentFound;

        public event Action OpponentLeft;

        public event Action<int> OpponentPointsUpdated;

        public event Action OpponentReady;

        public event Action<int> PointsUpdated;

        public event Action<int> QueueRemainingTimeChanged;

        #endregion Events

        #region Public Properties

        public ConnectionState ConnectionState
        {
            get; private set;
        }

        #endregion Public Properties

        #region Private Properties

        private string GroupName
        {
            get; set;
        }

        #endregion Private Properties

        #region Public Methods

        public void AcceptGame()
        {
            _gameProxy.Invoke("AcceptGame", GroupName);
        }

        public void LeaveQueue()
        {
            _gameProxy.Invoke("LeaveQueue");
        }

        public void MoveDown()
        {
            _gameProxy.Invoke<List<NumberCell>>("MoveDown", GroupName);
        }

        public void MoveLeft()
        {
            _gameProxy.Invoke<List<NumberCell>>("MoveLeft", GroupName);
        }

        public void MoveRight()
        {
            _gameProxy.Invoke<List<NumberCell>>("MoveRight", GroupName);
        }

        public void MoveUp()
        {
            _gameProxy.Invoke<List<NumberCell>>("MoveUp", GroupName);
        }

        public void Queue()
        {
            _gameProxy.Invoke("Queue", Environment.MachineName);
        }

        public void RejectGame()
        {
            _gameProxy.Invoke("RejectGame", GroupName);
        }

        #endregion Public Methods

        #region Private Methods

        private void HubConnectionStateChanged(StateChange stateChange)
        {
            ConnectionState = stateChange.NewState;
            RaiseConnectionStateChanged(stateChange);
        }

        private void OnGameStarted(StartGameInformation startGameInformation)
        {
            RaiseGameStarted(startGameInformation);
        }

        private void OnOpponentFound(OpponentFoundInformation opponentFoundInformation)
        {
            GroupName = opponentFoundInformation.GroupName;
            RaiseOpponentFound(opponentFoundInformation.OpponentName);
        }

        private void RaiseCellsChanged(List<NumberCell> cells)
        {
            var handler = CellsChanged;
            if (handler != null) handler(cells);
        }

        private void RaiseConnectionStateChanged(StateChange stateChange)
        {
            Action<StateChange> handler = ConnectionStateChanged;
            if (handler != null) handler(stateChange);
        }

        private void RaiseGameCanceled()
        {
            var handler = GameCanceled;
            if (handler != null) handler();
        }

        private void RaiseGameOver(bool win)
        {
            var handler = GameOver;
            if (handler != null) handler(win);
        }

        private void RaiseGameStarted(StartGameInformation startGameInformation)
        {
            var handler = GameStarted;
            if (handler != null) handler(startGameInformation);
        }

        private void RaiseOpponentCellsChanged(List<NumberCell> cells)
        {
            var handler = OpponentCellsChanged;
            if (handler != null) handler(cells);
        }

        private void RaiseOpponentFound(string opponentName)
        {
            Action<string> handler = OpponentFound;
            if (handler != null) handler(opponentName);
        }

        private void RaiseOpponentLeft()
        {
            var handler = OpponentLeft;
            if (handler != null) handler();
        }

        private void RaiseOpponentPointsUpdated(int remainingPoints)
        {
            var handler = OpponentPointsUpdated;
            if (handler != null) handler(remainingPoints);
        }

        private void RaiseOpponentReady()
        {
            Action handler = OpponentReady;
            if (handler != null) handler();
        }

        private void RaisePointsUpdated(int remainingPoints)
        {
            var handler = PointsUpdated;
            if (handler != null) handler(remainingPoints);
        }

        private void RaiseQueueRemainingTimeChanged(int remainingTime)
        {
            Action<int> handler = QueueRemainingTimeChanged;
            if (handler != null) handler(remainingTime);
        }

        #endregion Private Methods
    }
}