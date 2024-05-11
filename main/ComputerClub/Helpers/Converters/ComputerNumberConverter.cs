using CommunityToolkit.Mvvm.ComponentModel;
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace ComputerClub.Helpers
{
    public partial class ComputerNumberConverter : ObservableObject, IMultiValueConverter
    {
        [ObservableProperty]
        private static List<Computer> _computers = RepositoryServiceLocator.Resolve<ComputersRepository>().GetAll().ToList();

        public ComputerNumberConverter() 
        {
            AuthService.GetInstance().PropertyChanged += AuthService_PropertyChanged;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int id = (int)values[0];
            return GetComputerNumberById(id);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public int GetComputerNumberById(int id)
        {
            try
            {
                int index = Computers.ToList().FindIndex(c => c.Id == id);

                if (index == -1)
                {
                    UpdateSource();
                    index = GetComputerNumberById(id);
                }

                return index + 1;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Exception during calculating number of computer\n" + ex.Message);
            }

            return 0;
        }


        private void AuthService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(AuthService.CurrentClub))
            {
                return;
            }

            if(AuthService.GetInstance().CurrentClub == null) 
            {
                return;
            }

            UpdateSource();
        }

        private void UpdateSource()
        {
            Computers = RepositoryServiceLocator.Resolve<ComputersRepository>().GetAll().ToList();
        }
    }
}
