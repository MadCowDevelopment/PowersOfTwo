using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Client;
using PowersOfTwo.Core;
using WebService;

namespace PowersOfTwo
{
    public class GameProxy
    {
        private readonly IHubProxy _gameProxy;
        private readonly HubConnection _hubConnection;

        public GameProxy()
        {
            _hubConnection = new HubConnection("http://localhost:8369");
            //_hubConnection = new HubConnection("http://4096.azurewebsites.net/");
            //_hubConnection = new HubConnection("http://powersoftwo.apphb.com");
            //_hubConnection = new HubConnection("http://pc-mgr-2:8369");

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

            _hubConnection.Start();
        }

        private void HubConnectionStateChanged(StateChange stateChange)
        {
            ConnectionState = stateChange.NewState;
            RaiseConnectionStateChanged(stateChange);
        }

        public ConnectionState ConnectionState { get; private set; }

        public event Action<StateChange> ConnectionStateChanged;

        private void RaiseConnectionStateChanged(StateChange stateChange)
        {
            Action<StateChange> handler = ConnectionStateChanged;
            if (handler != null) handler(stateChange);
        }

        public void Queue()
        {
            _gameProxy.Invoke("Queue", "TEST");
        }

        private void OnGameStarted(StartGameInformation startGameInformation)
        {
            GroupName = startGameInformation.GroupName;
            RaiseGameStarted(startGameInformation);
        }

        private string GroupName { get; set; }

        public event Action<List<NumberCell>> OpponentCellsChanged;

        public event Action<int> OpponentPointsUpdated;

        private void RaiseOpponentPointsUpdated(int remainingPoints)
        {
            var handler = OpponentPointsUpdated;
            if (handler != null) handler(remainingPoints);
        }

        private void RaiseOpponentCellsChanged(List<NumberCell> cells)
        {
            var handler = OpponentCellsChanged;
            if (handler != null) handler(cells);
        }


        public event Action<List<NumberCell>> CellsChanged;

        private void RaiseCellsChanged(List<NumberCell> cells)
        {
            var handler = CellsChanged;
            if (handler != null) handler(cells);
        }

        public event Action<int> PointsUpdated;

        private void RaisePointsUpdated(int remainingPoints)
        {
            var handler = PointsUpdated;
            if (handler != null) handler(remainingPoints);
        }

        public event Action<bool> GameOver;

        public event Action<StartGameInformation> GameStarted;

        private void RaiseGameStarted(StartGameInformation startGameInformation)
        {
            var handler = GameStarted;
            if (handler != null) handler(startGameInformation);
        }

        private void RaiseGameOver(bool win)
        {
            var handler = GameOver;
            if (handler != null) handler(win);
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

        public void MoveDown()
        {
            _gameProxy.Invoke<List<NumberCell>>("MoveDown", GroupName);
        }

        public void LeaveQueue()
        {
            _gameProxy.Invoke("LeaveQueue");
        }
    }
}