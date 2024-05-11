using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ComputerClub.Helpers
{
    public class BoolToOpacityMaskConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool isEnabled = (bool)values[0];
            Color color = Color.FromArgb(176, 0, 0, 100);

            if (isEnabled)
            {
                return new SolidColorBrush(color);
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
