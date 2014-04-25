using System.Windows.Controls;
using System.Windows.Input;
using PowersOfTwo.ViewModels;

namespace PowersOfTwo.Views
{
    /// <summary>
    /// Interaction logic for ReplayPlayerView.xaml
    /// </summary>
    public partial class ReplayPlayerView
    {
        #region Constructors

        public ReplayPlayerView()
        {
            InitializeComponent();
        }

        #endregion Constructors

        private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var progressBar = sender as ProgressBar;
            if (progressBar == null) return;
            var position = e.GetPosition(progressBar);
            var player = (DataContext as ReplayPlayerViewModel).Player;
            var frame = position.X / progressBar.ActualWidth * player.TotalFrames;
            player.Seek((int) frame);
        }
    }
}