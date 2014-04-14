using System;
using System.Windows.Input;
using PowersOfTwo.Framework;

namespace PowersOfTwo.ViewModels
{
    public class OverlayViewModel : ObservableObject
    {
        public OverlayViewModel()
        {
            Visible = false;
            CloseCommand = new RelayCommand(p => HideOverlay());
        }

        public event Action Closed;

        private void RaiseClosed()
        {
            var handler = Closed;
            if (handler != null) handler();
        }

        public ObservableObject Content { get; private set; }

        public bool Visible { get; private set; }

        public ICommand CloseCommand { get; private set; }

        public void ShowOverlay(ObservableObject content)
        {
            Content = content;
            Visible = true;
        }

        private void HideOverlay()
        {
            Visible = false;
            RaiseClosed();
        }
    }
}
