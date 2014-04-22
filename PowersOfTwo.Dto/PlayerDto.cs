using System.Collections.Generic;
using PowersOfTwo.Core;

namespace PowersOfTwo.Dto
{
    public class PlayerDto
    {
        public string Name { get; private set; }
        public List<NumberCell> Cells { get; private set; }
        public int Points { get; set; }

        public PlayerDto(string name, List<NumberCell> cells, int points)
        {
            Name = name;
            Cells = cells;
            Points = points;
        }
    }
}