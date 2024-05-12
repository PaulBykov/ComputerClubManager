using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Exceptions;
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.View.modal;
using ComputerClub.ViewModel.modal;
using System;
using static ComputerClub.View.modal.NotifyModalWindow;


namespace ComputerClub.ViewModel
{
    public partial class AddRateWindowVM : ObservableObject, IModalWindowVM
    {
        [ObservableProperty]
        private string _rateName;

        [ObservableProperty]
        private double _price;

        public AddRateWindowVM() { }

        public event EventHandler Done;

        [RelayCommand]
        public void FormSubmit()
        {
            RatesRepository repository = RepositoryServiceLocator.Resolve<RatesRepository>();
            Rate rate = new Rate() { Name = RateName, Price = Price };

            if (repository.Has(rate.Name))
            {
                // this rate already exist
                throw new InvalidDataException("Rate with this name already exist");
            }

            repository.Add(rate);

            Done?.Invoke(this, EventArgs.Empty);
            NotifyModalWindow.Show(NotifyKind.Success, $"Вы успешно добавили тариф {RateName}");
        }
    }
}
