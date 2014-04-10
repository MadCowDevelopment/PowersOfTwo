using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using PowersOfTwo.Core;
using WebService;

namespace PowersOfTwo
{
    public class GameProxy
    {
        private IHubProxy _gameProxy;
        private HubConnection _hubConnection;

        public async void Queue()
        {
            //_hubConnection = new HubConnection("http://localhost:8369");
            //_hubConnection = new HubConnection("http://powersoftwo.apphb.com");
            _hubConnection = new HubConnection("http://pc-mgr-2:8369");

            _hubConnection.TraceLevel = TraceLevels.All;
            _hubConnection.TraceWriter = Console.Out;

            _gameProxy = _hubConnection.CreateHubProxy("GameHub");
            _gameProxy.On<StartGameInformation>("StartGame", OnGameStarted);
            _gameProxy.On<bool>("GameOver", RaiseGameOver);
            _gameProxy.On<int>("UpdatePoints", RaisePointsUpdated);
            _gameProxy.On<List<NumberCell>>("CellsChanged", RaiseCellsChanged);

            await _hubConnection.Start();
            await _gameProxy.Invoke("Queue", "TEST");
        }

        private void OnGameStarted(StartGameInformation startGameInformation)
        {
            GroupName = startGameInformation.GroupName;
            RaiseGameStarted(startGameInformation);
        }

        private string GroupName { get; set; }

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
    }
}