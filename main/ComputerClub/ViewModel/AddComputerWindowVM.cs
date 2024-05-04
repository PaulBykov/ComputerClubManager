using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Windows;


namespace ComputerClub.ViewModel
{
    public partial class AddComputerWindowVM : ObservableObject
    {
        [ObservableProperty]
        private int _count = 1;

        [ObservableProperty]
        private Rate _selectedRate = null;

        [ObservableProperty]
        public ObservableCollection<Rate> _rateList;

        public EventHandler Done;

        public AddComputerWindowVM() 
        {
            RateList = GetRates();
        }


        private ObservableCollection<Rate> GetRates() 
        {
            return new ObservableCollection<Rate>(
                    RepositoryServiceLocator.Resolve<RatesRepository>().GetAll()
                );
        }

        [RelayCommand]
        private void FormSubmit() 
        {
            try 
            {
                if(SelectedRate == null)
                {
                    throw new ArgumentNullException("Неизвестный тариф!");
                }

                ComputersRepository computersRepository = RepositoryServiceLocator.Resolve<ComputersRepository>();
                computersRepository.AddMany(Count, SelectedRate);
                Done?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e) 
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
