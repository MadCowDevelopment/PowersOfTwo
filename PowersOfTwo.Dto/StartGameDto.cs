using System.Collections.Generic;

using PowersOfTwo.Core;

namespace PowersOfTwo.Dto
{
    public class StartGameDto
    {
        #region Constructors

        public StartGameDto(string name, string opponentName, int startPoints, List<int?> cells, List<int?> opponentCells)
        {
            Cells = cells;
            OpponentCells = opponentCells;
            Name = name;
            OpponentName = opponentName;
            StartPoints = startPoints;
        }

        #endregion Constructors

        #region Public Properties

        public List<int?> Cells
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public List<int?> OpponentCells
        {
            get;
            private set;
        }

        public string OpponentName
        {
            get; set;
        }

        public int StartPoints
        {
            get;
            private set;
        }

        #endregion Public Properties
    }
}