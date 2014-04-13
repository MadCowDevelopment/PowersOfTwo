using PowersOfTwo.Framework;

namespace PowersOfTwo.ViewModels
{
    public class OverlayTextViewModel : ObservableObject
    {
        public string Text { get; private set; }

        public OverlayTextViewModel(string text)
        {
            Text = text;
        }
    }
}