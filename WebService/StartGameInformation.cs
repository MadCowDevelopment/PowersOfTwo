using System.Collections.Generic;
using PowersOfTwo.Core;

namespace WebService
{
    public class StartGameInformation
    {
        public string GroupName { get; private set; }
        public string Name { get; private set; }
        public int StartPoints { get; private set; }
        public List<NumberCell> Cells { get; private set; }

        public StartGameInformation(string groupName, string name, int startPoints, List<NumberCell> cells)
        {
            Cells = cells;
            GroupName = groupName;
            Name = name;
            StartPoints = startPoints;
        }
    }
}