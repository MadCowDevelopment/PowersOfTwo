using System.Collections.Generic;

using PowersOfTwo.Core;
using PowersOfTwo.Framework;

namespace PowersOfTwo.ViewModels
{
    public class PlayerViewModel : ObservableObject
    {
        public string Name { get; private set; }

        public PlayerViewModel(string name)
        {
            Name = name;
        }

        #region Public Properties

        public List<NumberCell> Cells
        {
            get;
            set;
        }

        public int Points
        {
            get;
            set;
        }

        #endregion Public Properties
    }
}