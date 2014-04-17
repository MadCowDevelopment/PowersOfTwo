using System;

using PowersOfTwo.Framework;

namespace PowersOfTwo.ViewModels
{
    public class OverlayViewModel : ObservableObject
    {
        #region Constructors

        public OverlayViewModel()
        {
            Visible = false;
        }

        #endregion Constructors

        #region Events

        public event Action Closed;

        #endregion Events

        #region Public Properties

        public ObservableObject Content
        {
            get; private set;
        }

        public bool Visible
        {
            get; private set;
        }

        #endregion Public Properties

        #region Public Methods

        public void Hide()
        {
            Visible = false;
            RaiseClosed();
        }

        public void Show(ObservableObject content)
        {
            Content = content;
            Visible = true;
        }

        #endregion Public Methods

        #region Private Methods

        private void RaiseClosed()
        {
            var handler = Closed;
            if (handler != null) handler();
        }

        #endregion Private Methods
    }
}