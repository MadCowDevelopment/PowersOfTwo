using PowersOfTwo.Framework;

namespace PowersOfTwo.ViewModels
{
    public class OverlayTextViewModel : ObservableObject
    {
        #region Constructors

        public OverlayTextViewModel(string text)
        {
            Text = text;
        }

        #endregion Constructors

        #region Public Properties

        public string Text
        {
            get; private set;
        }

        #endregion Public Properties
    }
}