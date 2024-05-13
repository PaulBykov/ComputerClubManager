using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.ViewModel.modal;
using System;
using System.Collections.ObjectModel;
using ComputerClub.View.modal;

using static ComputerClub.View.modal.NotifyModalWindow;


namespace ComputerClub.ViewModel
{
    public partial class AddComputerWindowVM : ObservableObject, IModalWindowVM
    {
        [ObservableProperty]
        private int _count = 1;

        [ObservableProperty]
        private Rate _selectedRate = null;

        [ObservableProperty]
        public ObservableCollection<Rate> _rateList;

        public event EventHandler Done;

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
                Logger.Add($"Добавил {Count} компьютеров тарифа {SelectedRate.Name}");
                NotifyModalWindow.Show(NotifyKind.Success,$"Вы успешно добавили компьютеры {Count} шт");
            }
            catch (Exception e) 
            {
                NotifyModalWindow.Show(NotifyKind.Error, $"Произошла ошибка при добавлении компьютеров: " + e.Message);
            }
        }
    }
}
