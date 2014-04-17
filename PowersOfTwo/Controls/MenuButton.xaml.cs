using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PowersOfTwo.Controls
{
    /// <summary>
    /// Interaction logic for MenuButton.xaml
    /// </summary>
    public partial class MenuButton : UserControl
    {
        #region Fields

        public static readonly DependencyProperty CommandProperty = 
            DependencyProperty.Register("Command", typeof(ICommand), typeof(MenuButton));
        public static readonly DependencyProperty ImageProperty = 
            DependencyProperty.Register("Image", typeof(string), typeof(MenuButton));
        public static readonly DependencyProperty TextProperty = 
            DependencyProperty.Register("Text", typeof(string), typeof(MenuButton));

        #endregion Fields

        #region Constructors

        public MenuButton()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Public Properties

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion Public Properties
    }
}