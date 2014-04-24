using System.Collections.Generic;

using PowersOfTwo.Core;
using PowersOfTwo.Framework;

namespace PowersOfTwo.ViewModels
{
    public class PlayerViewModel : ObservableObject
    {
        #region Constructors

        public PlayerViewModel(string name)
        {
            Name = name;
        }

        #endregion Constructors

        #region Public Properties

        public List<NumberCell> Cells
        {
            get;
            set;
        }

        public string Name
        {
            get; private set;
        }

        public int Points
        {
            get;
            set;
        }

        #endregion Public Properties
    }
}