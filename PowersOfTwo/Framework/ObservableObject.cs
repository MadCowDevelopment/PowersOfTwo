using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PowersOfTwo.Framework
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Protected Methods

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Protected Methods
    }
}