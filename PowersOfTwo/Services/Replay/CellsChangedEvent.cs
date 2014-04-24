using System.Collections.Generic;

using PowersOfTwo.Core;

namespace PowersOfTwo.Services.Replay
{
    public class CellsChangedEvent : ReplayEvent
    {
        #region Constructors

        public CellsChangedEvent(int playerNumber, List<int?> cells)
        {
            PlayerNumber = playerNumber;
            Cells = cells;
        }

        #endregion Constructors

        #region Public Properties

        public List<int?> Cells
        {
            get; set;
        }

        public int PlayerNumber
        {
            get; set;
        }

        #endregion Public Properties

        #region Public Methods

        public override void Replay(IReplayTarget replayTarget)
        {
            var player = PlayerNumber == 1 ? replayTarget.Player1 : replayTarget.Player2;
            player.Cells = Cells;
        }

        #endregion Public Methods
    }
}