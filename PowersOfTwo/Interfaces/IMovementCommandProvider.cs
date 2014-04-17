using System.Windows.Input;

namespace PowersOfTwo.Interfaces
{
    public interface IMovementCommandProvider
    {
        #region Properties

        ICommand DownCommand
        {
            get;
        }

        ICommand LeftCommand
        {
            get;
        }

        ICommand RightCommand
        {
            get;
        }

        ICommand UpCommand
        {
            get;
        }

        #endregion Properties
    }
}