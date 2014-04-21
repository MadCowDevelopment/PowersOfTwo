using System.ComponentModel;
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

        #region Private Properties

        private MainWindowViewModel MainWindowViewModel
        {
            get { return (DataContext as MainWindowViewModel); }
        }

        #endregion Private Properties

        #region Protected Methods

        protected override void OnClosing(CancelEventArgs e)
        {
            MainWindowViewModel.Dispose();
            base.OnClosing(e);
        }

        #endregion Protected Methods

        #region Private Methods

        private void MainWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
            }

            if (MainWindowViewModel.Overlay.Visible)
            {
                if (e.Key == Key.Enter)
                {
                    MainWindowViewModel.Overlay.Hide(true);
                }
                else if (e.Key == Key.Escape)
                {
                    MainWindowViewModel.Overlay.Hide(false);
                }

                e.Handled = true;
            }
        }

        #endregion Private Methods
    }
}