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
        private readonly StaffRepository _repository = RepositoryServiceLocator.Resolve<StaffRepository>();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Staff person = values[0] as Staff;
            return GetClubList(person);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string GetClubList(Staff person) 
        {
            IEnumerable<Staff> peopleList = _repository.GetAll();

            List<Club> clubList = peopleList.Where(p => p.Id.Equals(person.Id)).SelectMany(p => p.Clubs).ToList();

            return string.Join(", ", clubList);
        }
    }
}
