using System.Collections.Generic;

using PowersOfTwo.Core;

namespace PowersOfTwo.Dto
{
    public class PlayerDto
    {
        #region Constructors

        public PlayerDto(string name, List<int?> cells, int points)
        {
            Name = name;
            Cells = cells;
            Points = points;
        }

        #endregion Constructors

        #region Public Properties

        public List<int?> Cells
        {
            get; private set;
        }

        public string Name
        {
            get; private set;
        }

        public int Points
        {
            get; set;
        }

        #endregion Public Properties
    }
}