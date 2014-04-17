using System.Windows;
using System.Windows.Media;

namespace PowersOfTwo.Controls
{
    public partial class ScoreControl
    {
        #region Fields

        public static readonly DependencyProperty ImageProperty = 
            DependencyProperty.Register("Image", typeof(string), typeof(ScoreControl));
        public static readonly DependencyProperty PointsProperty = 
            DependencyProperty.Register("Points", typeof(int), typeof(ScoreControl));
        public static readonly DependencyProperty ShadowColorProperty = 
            DependencyProperty.Register("ShadowColor", typeof(Color), typeof(ScoreControl));
        public static readonly DependencyProperty TextProperty = 
            DependencyProperty.Register("Text", typeof(string), typeof(ScoreControl));

        #endregion Fields

        #region Constructors

        public ScoreControl()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Public Properties

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public int Points
        {
            get { return (int) GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }

        public Color ShadowColor
        {
            get { return (Color) GetValue(ShadowColorProperty); }
            set { SetValue(ShadowColorProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion Public Properties
    }
}