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

        private void MainWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                (DataContext as MainWindowViewModel).Overlay.Hide();
                e.Handled = true;
            }
        }
    }
}