using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.ViewModel.modal;
using System;
namespace ComputerClub.ViewModel
{
    public partial class EditRateWindowVM : ObservableObject, IModalWindowVM
    {
        [ObservableProperty]
        private string _rateName;

        [ObservableProperty]
        private double _price;

        private Rate _rate;

        public event EventHandler Done;

        public EditRateWindowVM(Rate rate) 
        {
            _rate = rate;

            RateName = rate.Name;
            Price = rate.Price;
        }

        [RelayCommand]
        private void FormSubmit() 
        {
            _rate.Name = RateName;
            _rate.Price = Price;

            RatesRepository repository = RepositoryServiceLocator.Resolve<RatesRepository>();
            repository.Update(_rate);

            Done?.Invoke(this, EventArgs.Empty);
        }
    }
}
