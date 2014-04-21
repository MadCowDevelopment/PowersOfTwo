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

        public event Action<bool?> Closed;

        #endregion Events

        #region Public Properties

        public bool AllowAccept
        {
            get; private set;
        }

        public bool AllowCancel
        {
            get; private set;
        }

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

        public void Hide(bool? result)
        {
            if (result.HasValue)
            {
                if (result.Value && !AllowAccept) return;
                if (!result.Value && !AllowCancel) return;
            }

            Visible = false;
            RaiseClosed(result);
        }

        public void Show(ObservableObject content, bool allowAccept, bool allowCancel)
        {
            AllowAccept = allowAccept;
            AllowCancel = allowCancel;

            Content = content;
            Visible = true;
        }

        #endregion Public Methods

        #region Private Methods

        private void RaiseClosed(bool? result)
        {
            var handler = Closed;
            if (handler != null) handler(result);
        }

        #endregion Private Methods
    }
}