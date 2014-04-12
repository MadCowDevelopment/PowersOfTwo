using System.Windows;
using System.Windows.Controls;

namespace PowersOfTwo.Controls
{
    /// <summary>
    /// Interaction logic for MenuHeader.xaml
    /// </summary>
    public partial class MenuHeader : UserControl
    {
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(string), typeof(MenuHeader));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MenuHeader));

        public MenuHeader()
        {
            InitializeComponent();
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
    }
}
