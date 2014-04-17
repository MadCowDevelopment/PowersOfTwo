using System;
using PowersOfTwo.Framework;

namespace PowersOfTwo.ViewModels
{
    public class OverlayViewModel : ObservableObject
    {
        public OverlayViewModel()
        {
            Visible = false;
            
        }

        public event Action Closed;

        private void RaiseClosed()
        {
            var handler = Closed;
            if (handler != null) handler();
        }

        public ObservableObject Content { get; private set; }

        public bool Visible { get; private set; }

        public void Show(ObservableObject content)
        {
            
            Content = content;
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
            RaiseClosed();
        }
    }
}
