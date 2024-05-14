using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Exceptions;
using ComputerClub.Model;
using ComputerClub.Repositories;
using static ComputerClub.View.modal.NotifyModalWindow;


namespace ComputerClub.ViewModel.modal
{
    public partial class AddRateWindowVM : ObservableValidator, IModalWindowVM
    {
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required (ErrorMessage = "Это обязательное поле")]
        [MinLength(3, ErrorMessage = "Минимальная длина 3")]
        [MaxLength(50, ErrorMessage = "Максимальная длина 50")]
        private string _rateName;

        [ObservableProperty]
        private double _price;

        public AddRateWindowVM() { }

        public event EventHandler Done;

        [RelayCommand]
        public void FormSubmit()
        {
            try
            {
                RatesRepository repository = RepositoryServiceLocator.Resolve<RatesRepository>();
                Rate rate = new Rate() { Name = RateName, Price = Price };

                if (repository.Has(rate.Name))
                {
                    // this rate already exist
                    throw new InvalidDataException("Тариф с таким именем уже существует");
                }

                repository.Add(rate);

                Done?.Invoke(this, EventArgs.Empty);
                Logger.Add($"Добавил новый тариф - {RateName}");
                Show(NotifyKind.Success, $"Вы успешно добавили тариф {RateName}");
            }
            catch (InvalidDataException e)
            {
                Show(NotifyKind.Error, $"{e.Message}");
            }
            catch (Exception e)
            {
                Show(NotifyKind.Error, $"Произошла ошибка при добавлении тарифа: {e.Message}");
            }
        }
    }
}
