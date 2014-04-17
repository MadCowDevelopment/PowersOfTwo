using System.Collections.Generic;

using PowersOfTwo.Core;
using PowersOfTwo.Framework;

namespace PowersOfTwo.ViewModels
{
    public class PlayerViewModel : ObservableObject
    {
        #region Public Properties

        public List<NumberCell> Cells
        {
            get; set;
        }

        public int Points
        {
            get; set;
        }

        #endregion Public Properties
    }
}