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

            await _hubConnection.Start();
            await _gameProxy.Invoke("Queue", "TEST");
        }

        private void OnGameStarted(StartGameInformation startGameInformation)
        {
            GroupName = startGameInformation.GroupName;
            RaiseGameStarted(startGameInformation);
        }

        private string GroupName { get; set; }

        public event Action<int> PointsUpdated;

        private void RaisePointsUpdated(int remainingPoints)
        {
            Action<int> handler = PointsUpdated;
            if (handler != null) handler(remainingPoints);
        }

        public event Action<bool> GameOver;

        public event Action<StartGameInformation> GameStarted;

        private void RaiseGameStarted(StartGameInformation startGameInformation)
        {
            Action<StartGameInformation> handler = GameStarted;
            if (handler != null) handler(startGameInformation);
        }

        private void RaiseGameOver(bool win)
        {
            Action<bool> handler = GameOver;
            if (handler != null) handler(win);
        }

        public async Task<List<NumberCell>> MoveLeft()
        {
            return await _gameProxy.Invoke<List<NumberCell>>("MoveLeft", GroupName);
        }

        public async Task<List<NumberCell>> MoveRight()
        {
            return await _gameProxy.Invoke<List<NumberCell>>("MoveRight", GroupName);
        }

        public async Task<List<NumberCell>> MoveUp()
        {
            return await _gameProxy.Invoke<List<NumberCell>>("MoveUp", GroupName);
        }

        public async Task<List<NumberCell>> MoveDown()
        {
            return await _gameProxy.Invoke<List<NumberCell>>("MoveDown", GroupName);
        }
    }
}