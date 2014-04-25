using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PowersOfTwo.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null) return value != null ? Visibility.Visible : Visibility.Collapsed;

            var boolParam = System.Convert.ToBoolean(parameter);
            return boolParam
                ? (value == null ? Visibility.Visible : Visibility.Collapsed)
                : (value != null ? Visibility.Visible : Visibility.Collapsed);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion Public Methods
    }
}