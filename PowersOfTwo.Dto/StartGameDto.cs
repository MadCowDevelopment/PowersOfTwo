using System.Collections.Generic;

using PowersOfTwo.Core;

namespace PowersOfTwo.Dto
{
    public class StartGameDto
    {
        #region Constructors

        public StartGameDto(string name, string opponentName, int startPoints, List<NumberCell> cells, List<NumberCell> opponentCells)
        {
            Cells = cells;
            OpponentCells = opponentCells;
            Name = name;
            OpponentName = opponentName;
            StartPoints = startPoints;
        }

        #endregion Constructors

        #region Public Properties

        public List<NumberCell> Cells
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public List<NumberCell> OpponentCells
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