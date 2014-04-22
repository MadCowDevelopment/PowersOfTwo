using System.Collections.Generic;
using PowersOfTwo.Core;

namespace PowersOfTwo.Services.Replay
{
    public class CellsChangedEvent : ReplayEvent
    {
        public int PlayerNumber { get; set; }
        public List<NumberCell> Cells { get; set; }

        public CellsChangedEvent(int playerNumber, List<NumberCell> cells)
        {
            PlayerNumber = playerNumber;
            Cells = cells;
        }

        public override void Replay(IReplayTarget replayTarget)
        {
            var player = PlayerNumber == 1 ? replayTarget.Player1 : replayTarget.Player2;
            player.Cells = Cells;
        }
    }
}