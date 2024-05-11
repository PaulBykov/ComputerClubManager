using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Effects;

namespace ComputerClub.Helpers.Converters
{
    public class BoolToBlurEffectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isEnabled = (bool)value;

            if (isEnabled)
            {
                return new BlurEffect() { Radius = 20 };
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
