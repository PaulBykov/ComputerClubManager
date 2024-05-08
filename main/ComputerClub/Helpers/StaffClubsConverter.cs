using ComputerClub.Model;
using ComputerClub.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace ComputerClub.Helpers
{
    public class StaffClubsConverter : IMultiValueConverter
    {
        private readonly UserRepository _repository = RepositoryServiceLocator.Resolve<UserRepository>();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            User person = values[0] as User;
            return GetClubList(person);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string GetClubList(User person) 
        {

            List<Club> clubList = person.Clubs.ToList();

            return string.Join(", ", clubList);
        }
    }
}
