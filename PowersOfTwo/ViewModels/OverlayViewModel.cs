using System;

using PowersOfTwo.Framework;

namespace PowersOfTwo.ViewModels
{
    public class OverlayViewModel : ObservableObject
    {
        private Action<bool?> _callback;

        #region Constructors

        public OverlayViewModel()
        {
            Visible = false;
        }

        #endregion Constructors

        #region Public Properties

        public bool AllowAccept
        {
            get;
            private set;
        }

        public bool AllowCancel
        {
            get;
            private set;
        }

        public ObservableObject Content
        {
            get;
            private set;
        }

        public bool Visible
        {
            get;
            private set;
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
            if (_callback != null) _callback(result);
        }

        public void Show(
            ObservableObject content,
            Action<bool?> callback = null,
            bool allowAccept = true,
            bool allowCancel = false)
        {
            _callback = callback;

            AllowAccept = allowAccept;
            AllowCancel = allowCancel;

            Content = content;
            Visible = true;
        }

        #endregion Public Methods
    }
}