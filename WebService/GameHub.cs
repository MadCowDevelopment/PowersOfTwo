using System;
using System.Collections.Generic;

using Microsoft.AspNet.SignalR;

using PowersOfTwo.Core;

namespace WebService
{
    public class GameHub : Hub
    {
        #region Fields

        private const int Columns = 4;
        private const int Rows = 4;
        private const int StartPoints = 1024;

        private static readonly Queue<Player> QueuedPlayers = new Queue<Player>();
        private static readonly object QueueSyncLock = new object();
        private static readonly Dictionary<string, GameInformation> RunningGames = new Dictionary<string, GameInformation>();

        #endregion Fields

        #region Public Methods

        public void LeaveQueue(string userName)
        {
            // TODO
        }

        public List<NumberCell> MoveDown(string groupName)
        {
            GameInformation game;
            if (!RunningGames.TryGetValue(groupName, out game)) return new List<NumberCell>();

            lock (game)
            {
                if (game.IsFinished) return new List<NumberCell>();
                var player = game.GetPlayer(Context.ConnectionId);
                if (player == null) return new List<NumberCell>();

                player.GameLogic.MoveDown();
                return player.GameLogic.Cells;
            }
        }

        public List<NumberCell> MoveLeft(string groupName)
        {
            GameInformation game;
            if (!RunningGames.TryGetValue(groupName, out game)) return new List<NumberCell>();

            lock (game)
            {
                if (game.IsFinished) return new List<NumberCell>();
                var player = game.GetPlayer(Context.ConnectionId);
                if (player == null) return new List<NumberCell>();

                player.GameLogic.MoveLeft();
                return player.GameLogic.Cells;
            }
        }

        public List<NumberCell> MoveRight(string groupName)
        {
            GameInformation game;
            if (!RunningGames.TryGetValue(groupName, out game)) return new List<NumberCell>();

            lock (game)
            {
                if (game.IsFinished) return new List<NumberCell>();
                var player = game.GetPlayer(Context.ConnectionId);
                if (player == null) return new List<NumberCell>();

                player.GameLogic.MoveRight();
                return player.GameLogic.Cells;
            }
        }

        public List<NumberCell> MoveUp(string groupName)
        {
            GameInformation game;
            if (!RunningGames.TryGetValue(groupName, out game)) return new List<NumberCell>();

            lock (game)
            {
                if (game.IsFinished) return new List<NumberCell>();
                var player = game.GetPlayer(Context.ConnectionId);
                if (player == null) return new List<NumberCell>();
                player.GameLogic.MoveUp();
                return player.GameLogic.Cells;
            }
        }

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

        public void QuitGame(string groupName)
        {
            // TODO
        }

        #endregion Public Methods

        #region Private Methods

        private void StartNewGame(Player player1, Player player2)
        {
            var groupName = Guid.NewGuid().ToString();
            var gameInfo = new GameInformation(groupName, player1, player2);

            player1.GameLogic = new GameLogic(Rows, Columns);
            player1.GameLogic.CellsMatched += points => UpdateGame(player1, points, gameInfo);
            player1.GameLogic.OutOfMoves += () =>
            {
                gameInfo.IsFinished = true;
                SendCells(player1, player2);
                Clients.Client(player1.ConnectionId).GameOver(false);
                Clients.Client(player2.ConnectionId).GameOver(true);
            };

            player2.GameLogic = new GameLogic(Rows, Columns);
            player2.GameLogic.CellsMatched += points => UpdateGame(player2, points, gameInfo);
            player2.GameLogic.OutOfMoves += () =>
            {
                gameInfo.IsFinished = true;
                SendCells(player2, player1);
                Clients.Client(player1.ConnectionId).GameOver(true);
                Clients.Client(player2.ConnectionId).GameOver(false);
            };

            RunningGames.Add(groupName, gameInfo);

            Clients.Client(player1.ConnectionId)
                .StartGame(new StartGameInformation(groupName, player2.Name, StartPoints, player1.GameLogic.Cells));
            Clients.Client(player2.ConnectionId)
                .StartGame(new StartGameInformation(groupName, player1.Name, StartPoints, player2.GameLogic.Cells));
        }

        private void SendCells(Player currentPlayer, Player otherPlayer)
        {
            Clients.Client(currentPlayer.ConnectionId).CellsChanged(currentPlayer.GameLogic.Cells);
            Clients.Client(otherPlayer.ConnectionId).OpponentCellsChanged(currentPlayer.GameLogic.Cells);
        }

        private void UpdateGame(Player player, int points, GameInformation game)
        {
            if (game.IsFinished) return;
            if (player == null) return;
            var otherPlayer = game.OtherPlayer(player);
            if (otherPlayer == null) return;

            player.RemainingPoints += (points * 3 / 4);
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

        #endregion Private Methods
    }
}