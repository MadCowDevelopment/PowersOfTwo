using System.Collections.Generic;
using PowersOfTwo.Core;

namespace PowersOfTwo
{
    public class PlayerViewModel : ViewModel
    {
        private List<NumberCell> _cells;
        private int _remainingPoints;

        public List<NumberCell> Cells
        {
            get { return _cells; }
            set { _cells = value; OnPropertyChanged(); }
        }

        public int RemainingPoints
        {
            get { return _remainingPoints; }
            set
            {
                _remainingPoints = value; OnPropertyChanged();
            }
        }
    }
}
