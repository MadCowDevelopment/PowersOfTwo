using System.Collections.Generic;
using PowersOfTwo.Core;

namespace PowersOfTwo
{
    public class PlayerViewModel : ViewModel
    {
        public List<NumberCell> Cells { get; set; }

        public int Points { get; set; }
    }
}
