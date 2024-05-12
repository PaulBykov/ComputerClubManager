using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.Services;
using ComputerClub.View.windows;
using System.Collections.Generic;
using ComputerClub.Exceptions;
using ComputerClub.View.modal;

namespace ComputerClub.ViewModel
{
    public partial class RatesPageVM : ObservableObject
    {
        [ObservableProperty]
        private IEnumerable<Rate> _rates;

        public RatesPageVM() 
        {
            Repository = RepositoryServiceLocator.Resolve<RatesRepository>();
            UpdateRates();
        }

        private RatesRepository Repository { get; set; }

        [RelayCommand]
        private void AddRate() 
        {
            AddRateModalWindow window = new AddRateModalWindow();
            Effector.TryApplyModalEffects(window);

            UpdateRates();
        }

        [RelayCommand]
        private void RemoveRate(object rate)
        {
            try
            {
                Rate selectedRate = (Rate)rate;

                if (selectedRate.Computers.Count > 0)
                {
                    throw new CRUDException("Cannot delete rate: Linked computers exist!");
                }

                Repository.Delete(selectedRate);

                UpdateRates();
            }
            catch (CRUDException e)
            {
                NotifyModalWindow.Show(NotifyModalWindow.NotifyKind.Error, "С данным тарифом связаны компьютеры, удаление невозможно.");
            }
            catch (Exception e)
            {
                NotifyModalWindow.Show(NotifyModalWindow.NotifyKind.Error, e.Message);
            }
        }

        [RelayCommand]
        private void EditRate(object rate)
        {
            Rate selectedRate = (Rate)rate;
            EditRateModalWindow window = new EditRateModalWindow(selectedRate);
            Effector.TryApplyModalEffects(window);

            UpdateRates();
        }



        private void UpdateRates() 
        {
            Rates = Repository.GetAll();
        }
    }
}
