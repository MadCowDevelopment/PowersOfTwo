using System.Windows.Input;

using PowersOfTwo.ViewModels;

namespace PowersOfTwo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }

        #endregion Constructors

        #region Private Methods

        private void MainWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var mainWindowViewModel = (DataContext as MainWindowViewModel);
            if (mainWindowViewModel == null) return;

            if (e.Key == Key.Tab)
            {
                e.Handled = true;
            }

            if (mainWindowViewModel.Overlay.Visible)
            {
                if (e.Key == Key.Enter)
                {
                    mainWindowViewModel.Overlay.Hide(true);
                }
                else if (e.Key == Key.Escape)
                {
                    mainWindowViewModel.Overlay.Hide(false);
                }

                e.Handled = true;
            }
        }

        #endregion Private Methods
    }
}