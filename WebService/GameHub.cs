using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR;

using PowersOfTwo.Core;
using PowersOfTwo.Dto;

namespace WebService
{
    public class GameHub : Hub
    {
        #region Fields

        private const int Columns = 4;
        private const int Rows = 4;
        private const int StartPoints = 2048;

        private static readonly List<Player> QueuedPlayers = new List<Player>();
        private static readonly Dictionary<string, ReadyGameInformation> ReadyGames =
            new Dictionary<string, ReadyGameInformation>();
        private static readonly Dictionary<string, GameInformation> RunningGames =
            new Dictionary<string, GameInformation>();

        #endregion Fields

        #region Enumerations

        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        #endregion Enumerations

        #region Public Methods

        public void AcceptGame(string groupName)
        {
            ReadyGameInformation readyGameInfo;
            if (!ReadyGames.TryGetValue(groupName, out readyGameInfo)) return;
            var gameInfo = readyGameInfo.GameInformation;

            var player1 = gameInfo.GetPlayer(Context.ConnectionId);
            var player2 = gameInfo.OtherPlayer(player1);

            player1.IsReady = true;
            Clients.Client(player2.ConnectionId).OpponentReady();

            if (!player1.IsReady || !player2.IsReady) return;

            RunningGames.Add(groupName, gameInfo);

            DisposeReadyGame(readyGameInfo);

            Clients.Client(player1.ConnectionId)
                .StartGame(new StartGameDto(player1.Name, player2.Name, StartPoints, player1.GameLogic.Cells,
                    player2.GameLogic.Cells));
            Clients.Client(player2.ConnectionId)
                .StartGame(new StartGameDto(player2.Name, player1.Name, StartPoints, player2.GameLogic.Cells,
                    player1.GameLogic.Cells));
        }

        public void LeaveQueue()
        {
            lock (QueuedPlayers)
            {
                var player = QueuedPlayers.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId);
                QueuedPlayers.Remove(player);
            }
        }

        public Task<List<RunningGameDto>> GetRunningGames()
        {
            var getGamesTask = new TaskFactory<List<RunningGameDto>>().StartNew(() =>
                RunningGames.Values.Select(ConvertGameInfoToRunningGameDto).ToList());

            return getGamesTask;
        }

        public Task<RunningGameDto> GetRunningGame(string groupName)
        {
            var getGameTask = new TaskFactory<RunningGameDto>().StartNew(() =>
            {
                GameInformation game;
                if (!RunningGames.TryGetValue(groupName, out game)) return null;
                if (game.IsFinished) return null;
                return ConvertGameInfoToRunningGameDto(game);
            });

            return getGameTask;
        }

        private RunningGameDto ConvertGameInfoToRunningGameDto(GameInformation p)
        {
            return new RunningGameDto(p.GroupName,
                new PlayerDto(p.Player1.Name, p.Player1.GameLogic.Cells, p.Player1.RemainingPoints),
                new PlayerDto(p.Player2.Name, p.Player2.GameLogic.Cells, p.Player2.RemainingPoints));
        }

        public void JoinAsSpectator(string groupName, string name)
        {
            GameInformation game;
            if (!RunningGames.TryGetValue(groupName, out game)) return;
            if (game.IsFinished) return;

            game.Spectators.Add(new Spectator(Context.ConnectionId, name));
        }

        public void StopSpectating(string groupName)
        {
            GameInformation game;
            if (!RunningGames.TryGetValue(groupName, out game)) return;
            if (game.IsFinished) return;

            var spectator = game.Spectators.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId);
            if (spectator == null) return;
            game.Spectators.Remove(spectator);
        }

        public void MoveDown(string groupName)
        {
            Move(groupName, Direction.Down);
        }

        public void MoveLeft(string groupName)
        {
            Move(groupName, Direction.Left);
        }

        public void MoveRight(string groupName)
        {
            Move(groupName, Direction.Right);
        }

        public void MoveUp(string groupName)
        {
            Move(groupName, Direction.Up);
        }

        public override Task OnDisconnected()
        {
            lock (QueuedPlayers)
            {
                var players = QueuedPlayers.Where(p => p.ConnectionId == Context.ConnectionId).ToList();
                foreach (var player in players)
                {
                    QueuedPlayers.Remove(player);
                }
            }

            lock (ReadyGames)
            {
                var games =
                    ReadyGames.Where(p => p.Value.GameInformation.HasAnyPlayerConnectionId(Context.ConnectionId))
                        .ToList();
                foreach (var keyValuePair in games)
                {
                    RejectGame(keyValuePair.Key);
                }
            }

            lock (RunningGames)
            {
                var games = RunningGames.Where(p => p.Value.HasAnyPlayerConnectionId(Context.ConnectionId)).ToList();
                foreach (var keyValuePair in games)
                {
                    var loser = keyValuePair.Value.GetPlayer(Context.ConnectionId);
                    var winner = keyValuePair.Value.OtherPlayer(loser);
                    EndGame(keyValuePair.Value, winner, loser);
                }
            }

            return base.OnDisconnected();
        }

        public void Queue(string userName)
        {
            lock (QueuedPlayers)
            {
                var player1 = new Player(Context.ConnectionId, userName, StartPoints);

                if (QueuedPlayers.Count >= 1)
                {
                    var player2 = QueuedPlayers.First();
                    QueuedPlayers.Remove(player2);
                    PrepareNewGame(player1, player2);
                }
                else
                {
                    QueuedPlayers.Add(player1);
                }
            }
        }

        public void QuitGame(string groupName)
        {
            // TODO
        }

        public void RejectGame(string groupName)
        {
            ReadyGameInformation readyGameInfo;
            if (!ReadyGames.TryGetValue(groupName, out readyGameInfo)) return;

            var gameInfo = readyGameInfo.GameInformation;
            DisposeReadyGame(readyGameInfo);

            var otherPlayer = gameInfo.OtherPlayer(gameInfo.GetPlayer(Context.ConnectionId));
            Clients.Client(otherPlayer.ConnectionId).OpponentLeft();
        }

        #endregion Public Methods

        #region Private Methods

        private void DisposeReadyGame(ReadyGameInformation readyGameInfo)
        {
            ReadyGames.Remove(readyGameInfo.GameInformation.GroupName);
            readyGameInfo.RemainingTimeChanged -= ReadyGameInformationRemainingTimeChanged;
            readyGameInfo.Dispose();
        }

        private void EndGame(GameInformation gameInfo, Player winner, Player loser)
        {
            gameInfo.IsFinished = true;
            SendCells(gameInfo, loser, winner);
            SendGameOver(gameInfo, winner, loser);
        }

        private void Move(string groupName, Direction direction)
        {
            GameInformation game;
            if (!RunningGames.TryGetValue(groupName, out game)) return;
            if (game.IsFinished) return;
            var player = game.GetPlayer(Context.ConnectionId);
            if (player == null) return;

            lock (player.GameLogic)
            {
                switch (direction)
                {
                    case Direction.Up:
                        player.GameLogic.MoveUp();
                        break;
                    case Direction.Down:
                        player.GameLogic.MoveDown();
                        break;
                    case Direction.Left:
                        player.GameLogic.MoveLeft();
                        break;
                    case Direction.Right:
                        player.GameLogic.MoveRight();
                        break;
                }

                SendCells(game, player, game.OtherPlayer(player));
            }
        }

        private void PrepareNewGame(Player player1, Player player2)
        {
            var groupName = Guid.NewGuid().ToString();
            var gameInfo = new GameInformation(groupName, player1, player2);

            player1.GameLogic = new GameLogic(Rows, Columns);
            player1.GameLogic.CellsMatched += points => UpdateGame(player1, points, gameInfo);
            player1.GameLogic.OutOfMoves += () => EndGame(gameInfo, player2, player1);

            player2.GameLogic = new GameLogic(Rows, Columns);
            player2.GameLogic.CellsMatched += points => UpdateGame(player2, points, gameInfo);
            player2.GameLogic.OutOfMoves += () => EndGame(gameInfo, player1, player2);

            var readyGameInformation = new ReadyGameInformation(gameInfo);
            readyGameInformation.RemainingTimeChanged += ReadyGameInformationRemainingTimeChanged;
            ReadyGames.Add(groupName, readyGameInformation);

            Clients.Client(player1.ConnectionId).OpponentFound(new OpponentFoundDto(groupName, player2.Name));
            Clients.Client(player2.ConnectionId).OpponentFound(new OpponentFoundDto(groupName, player1.Name));
        }

        private void ReadyGameInformationRemainingTimeChanged(object sender, RemainingTimeChangedEventArgs args)
        {
            if (args.RemainingTime > 0)
            {
                Clients.Client(args.GameInformation.Player1.ConnectionId).QueueRemainingTimeChanged(args.RemainingTime);
                Clients.Client(args.GameInformation.Player2.ConnectionId).QueueRemainingTimeChanged(args.RemainingTime);
            }
            else
            {
                var readyGameInfo = sender as ReadyGameInformation;
                DisposeReadyGame(readyGameInfo);
                Clients.Client(args.GameInformation.Player1.ConnectionId).GameCanceled();
                Clients.Client(args.GameInformation.Player2.ConnectionId).GameCanceled();
            }
        }

        private void SendCells(GameInformation gameInfo, Player currentPlayer, Player otherPlayer)
        {
            Clients.Client(currentPlayer.ConnectionId).CellsChanged(currentPlayer.GameLogic.Cells);
            Clients.Client(otherPlayer.ConnectionId).OpponentCellsChanged(currentPlayer.GameLogic.Cells);

            foreach (var spectator in gameInfo.Spectators)
            {
                var playerNumber = currentPlayer == gameInfo.Player1 ? 1 : 2;
                if (playerNumber == 1)
                {
                    Clients.Client(spectator.ConnectionId).CellsChanged(currentPlayer.GameLogic.Cells);
                }
                else
                {
                    Clients.Client(spectator.ConnectionId).OpponentCellsChanged(currentPlayer.GameLogic.Cells);
                }
            }
        }

        private void SendGameOver(GameInformation gameInfo, Player winner, Player loser)
        {
            gameInfo.IsFinished = true;
            lock (RunningGames)
            {
                if (RunningGames.ContainsKey(gameInfo.GroupName)) RunningGames.Remove(gameInfo.GroupName);
            }

            Clients.Client(winner.ConnectionId).GameOver(true);
            Clients.Client(loser.ConnectionId).GameOver(false);

            foreach (var spectator in gameInfo.Spectators)
            {
                var playerNumber = winner == gameInfo.Player1 ? 1 : 2;
                if (playerNumber == 1)
                {
                    Clients.Client(spectator.ConnectionId).GameOver(true);
                }
                else
                {
                    Clients.Client(spectator.ConnectionId).GameOver(false);
                }
            }
        }

        private void SendUpdatePoints(GameInformation gameInfo, Player player, Player otherPlayer)
        {
            Clients.Client(player.ConnectionId).UpdatePoints(player.RemainingPoints);
            Clients.Client(otherPlayer.ConnectionId).UpdatePoints(otherPlayer.RemainingPoints);

            Clients.Client(player.ConnectionId).UpdateOpponentPoints(otherPlayer.RemainingPoints);
            Clients.Client(otherPlayer.ConnectionId).UpdateOpponentPoints(player.RemainingPoints);

            foreach (var spectator in gameInfo.Spectators)
            {
                var playerNumber = player == gameInfo.Player1 ? 1 : 2;
                if (playerNumber == 1)
                {
                    Clients.Client(spectator.ConnectionId).UpdatePoints(player.RemainingPoints);
                    Clients.Client(spectator.ConnectionId).UpdateOpponentPoints(otherPlayer.RemainingPoints);
                }
                else
                {
                    Clients.Client(spectator.ConnectionId).UpdatePoints(otherPlayer.RemainingPoints);
                    Clients.Client(spectator.ConnectionId).UpdateOpponentPoints(player.RemainingPoints);
                }
            }
        }

        private void UpdateGame(Player player, int points, GameInformation game)
        {
            if (game.IsFinished) return;
            if (player == null) return;
            var otherPlayer = game.OtherPlayer(player);
            if (otherPlayer == null) return;

            player.RemainingPoints += (points / 3 * 2);
            otherPlayer.RemainingPoints -= points;

            if (otherPlayer.RemainingPoints <= 0)
            {
                game.IsFinished = true;
                SendGameOver(game, player, otherPlayer);
            }
            else
            {
                SendUpdatePoints(game, player, otherPlayer);
            }
        }

        #endregion Private Methods
    }
}