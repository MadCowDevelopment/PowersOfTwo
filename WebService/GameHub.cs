using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;

namespace WebService
{
    public class GameHub : Hub
    {
        private const int StartPoints = 1024;

        private static readonly Queue<Player> QueuedPlayers = new Queue<Player>();

        private static readonly Dictionary<string, GameInformation> RunningGames = new Dictionary<string, GameInformation>();

        private static readonly object QueueSyncLock = new object();

        public void Queue(string userName)
        {
            lock (QueueSyncLock)
            {
                var player1 = new Player(Context.ConnectionId, userName, StartPoints);

                if (QueuedPlayers.Count >= 1)
                {
                    var player2 = QueuedPlayers.Dequeue();
                    StartNewGame(player1, player2);
                }
                else
                {
                    QueuedPlayers.Enqueue(player1);
                }
            }
        }

        public void MatchTiles(string groupName, int points)
        {
            GameInformation game;
            if (!RunningGames.TryGetValue(groupName, out game)) return;

            lock (game)
            {
                UpdateGame(points, game);
            }
        }

        public void OutOfMoves(string groupName)
        {
            GameInformation game;
            if (!RunningGames.TryGetValue(groupName, out game)) return;

            lock (game)
            {
                if (game.IsFinished) return;
                var player = game.GetPlayer(Context.ConnectionId);
                if (player == null) return;
                var otherPlayer = game.OtherPlayer(player);
                if (otherPlayer == null) return;

                game.IsFinished = true;
                Clients.Client(player.ConnectionId).GameOver(false);
                Clients.Client(otherPlayer.ConnectionId).GameOver(true);
            }
        }

        public void LeaveQueue(string userName)
        {
            // TODO
        }

        public void QuitGame(string groupName)
        {
            // TODO
        }

        private void UpdateGame(int points, GameInformation game)
        {
            if (game.IsFinished) return;
            var player = game.GetPlayer(Context.ConnectionId);
            if (player == null) return;
            var otherPlayer = game.OtherPlayer(player);
            if (otherPlayer == null) return;

            player.RemainingPoints += points;
            otherPlayer.RemainingPoints -= points;

            if (otherPlayer.RemainingPoints <= 0)
            {
                game.IsFinished = true;
                Clients.Client(player.ConnectionId).GameOver(true);
                Clients.Client(otherPlayer.ConnectionId).GameOver(false);
            }
            else
            {
                Clients.Client(player.ConnectionId).UpdatePoints(player.RemainingPoints);
                Clients.Client(otherPlayer.ConnectionId).UpdatePoints(otherPlayer.RemainingPoints);
            }
        }

        private void StartNewGame(Player player1, Player player2)
        {
            var groupName = Guid.NewGuid().ToString();
            RunningGames.Add(groupName, new GameInformation(groupName, player1, player2));

            Clients.Client(player1.ConnectionId)
                .StartGame(new StartGameInformation(groupName, player2.Name, StartPoints));
            Clients.Client(player2.ConnectionId)
                .StartGame(new StartGameInformation(groupName, player1.Name, StartPoints));
        }
    }
}