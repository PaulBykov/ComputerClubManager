using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ComputerClub.Model;
using ComputerClub.Services;

namespace ComputerClub.Helpers.Converters
{
    public class UserToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            User currentUser = AuthService.GetInstance().CurrentUser;
            User user = ((DataGridRow)value)?.Item as User;
            
            if (user.Equals(currentUser))
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible; // Оставляем элемент видимым
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
