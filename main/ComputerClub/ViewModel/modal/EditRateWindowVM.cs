using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.View.modal;

namespace ComputerClub.ViewModel.modal
{
    public partial class EditRateWindowVM : ObservableValidator, IModalWindowVM
    {
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required (ErrorMessage = "Это обязательное поле")]
        [MinLength(3, ErrorMessage = "Минимальная длина 3")]
        [MaxLength(50, ErrorMessage = "Максимальная длина 50")]
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
            try
            {
                _rate.Name = RateName;
                _rate.Price = Price;

                RatesRepository repository = RepositoryServiceLocator.Resolve<RatesRepository>();
                repository.Update(_rate);

                
                Logger.Add($"Изменил тариф {RateName}");
                Done?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                NotifyModalWindow.Show(NotifyModalWindow.NotifyKind.Success, "Произошла ошибка при изменеии тарифа: " + e.Message);
            }
        }
    }
}
