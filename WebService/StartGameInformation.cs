using System.Collections.Generic;

using PowersOfTwo.Core;

namespace WebService
{
    public class StartGameInformation
    {
        #region Constructors

        public StartGameInformation(string name, int startPoints, List<NumberCell> cells, List<NumberCell> opponentCells)
        {
            Cells = cells;
            OpponentCells = opponentCells;
            Name = name;
            StartPoints = startPoints;
        }

        #endregion Constructors

        #region Public Properties

        public List<NumberCell> Cells
        {
            get; private set;
        }

        public string Name
        {
            get; private set;
        }

        public List<NumberCell> OpponentCells
        {
            get; private set;
        }

        public int StartPoints
        {
            get; private set;
        }

        #endregion Public Properties
    }

    public class OpponentFoundInformation
    {
        #region Constructors

        public OpponentFoundInformation(string groupName, string opponentName)
        {
            GroupName = groupName;
            OpponentName = opponentName;
        }

        #endregion Constructors

        #region Public Properties

        public string GroupName
        {
            get; private set;
        }

        public string OpponentName
        {
            get; private set;
        }

        #endregion Public Properties
    }
}