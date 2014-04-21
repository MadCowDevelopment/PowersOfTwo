using PowersOfTwo.Framework;

namespace PowersOfTwo.ViewModels
{
    public class OverlayTextViewModel : ObservableObject
    {
        #region Constructors

        public OverlayTextViewModel(string text, int fontSize)
        {
            Text = text;
            FontSize = fontSize;
        }

        #endregion Constructors

        #region Public Properties

        public int FontSize
        {
            get; private set;
        }

        public string Text
        {
            get; private set;
        }

        #endregion Public Properties
    }
}