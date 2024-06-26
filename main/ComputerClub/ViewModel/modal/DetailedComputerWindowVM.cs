﻿using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Helpers;
using ComputerClub.Model;
using ComputerClub.Properties;
using ComputerClub.Repositories;
using ComputerClub.Services;
using ComputerClub.View.modal;
using static ComputerClub.View.modal.NotifyModalWindow;

namespace ComputerClub.ViewModel.modal
{
    public partial class DetailedComputerWindowVM : ObservableObject, IModalWindowVM
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(AntiEditMode))]
        [NotifyPropertyChangedFor(nameof(RentalStartTime))]
        [NotifyPropertyChangedFor(nameof(RentDuration))]
        private bool _editMode = false;

        [ObservableProperty]
        private DateTime _rentalStartTime;

        [ObservableProperty]
        private TimeSpan _rentDuration;

        [ObservableProperty]
        private Rate _selectedRate;

        [ObservableProperty]
        private string _timeLeft = "00:00:00";

        [ObservableProperty]
        private IEnumerable<Rate> _rates;


        private readonly Computer _computer;
        private readonly ComputerNumberConverter _converter = new ComputerNumberConverter();
        private readonly RentTimer _timer;

        public event EventHandler Done;
        public event EventHandler ShouldSaveChanges;

        public DetailedComputerWindowVM(Computer computer)
        {
            _computer = computer;

            UpdateRentTime();
            TimeLeft = CalculateLeftTime();

            Rates = RepositoryServiceLocator.Resolve<RatesRepository>().GetAll();
            SelectedRate = computer.RateNameNavigation;

            _timer = new RentTimer(() => TimeLeft = CalculateLeftTime());
            _timer.Start();
        }

        ~DetailedComputerWindowVM()
        {
            _timer.Stop();
        }


        public int ComputerNumber => _converter.GetComputerNumberById(_computer.Id);
        public bool AntiEditMode => !EditMode;
        public string FormatedRentStartTime => RentalStartTime.TimeOfDay.ToString("hh\\:mm");


        [RelayCommand]
        private void EnableEditMode()
        {
            SetEditMode(true);
        }

        [RelayCommand]
        private void DeleteComputer() 
        {
            try
            {
                if (ConfirmationModalWindow.Show("Вы действительно хотите удалить компьютер?") != true)
                {
                    Done?.Invoke(this, EventArgs.Empty);
                    return;
                }

                ComputersRepository repository = RepositoryServiceLocator.Resolve<ComputersRepository>();

                repository.Delete(_computer);
                Logger.Add($"Удалил компьютер");
                Done?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e) 
            {
                System.Windows.MessageBox.Show("Error during deleting computer: " + e.Message);
            }
        }

        [RelayCommand]
        private void ConfirmChanges() 
        {
            try
            {
                RentsRepository rentsRepository = RepositoryServiceLocator.Resolve<RentsRepository>();
                ShouldSaveChanges?.Invoke(this, EventArgs.Empty);

                _computer.RateName = SelectedRate.Name;
                _computer.RateNameNavigation = SelectedRate;


                Rent rent = new Rent()
                {
                    Computer = _computer,
                    ComputerId = _computer.Id,
                    Length = TimeOnly.FromTimeSpan(RentDuration),
                    StartTime = RentalStartTime
                };
                

                double durationHours = rent.Length.ToTimeSpan().TotalHours;
                if (_computer.Rent != null)
                {
                    durationHours -= _computer.Rent.Length.ToTimeSpan().TotalHours;
                }

                double totalPrice = SelectedRate.Price * durationHours;
                totalPrice = Math.Round(totalPrice, 2);

                

                if (ConfirmationModalWindow.Show(
                        $"К оплате: {totalPrice} {Settings.Default.Currency}") != true)
                {
                    return;
                }

                IncomeRepository incomeRepository = RepositoryServiceLocator.Resolve<IncomeRepository>();
                incomeRepository.Add(new Income()
                {
                    Amount = Convert.ToDecimal(totalPrice),
                    ClubId = rent.Computer.ClubId,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    RateName = rent.Computer.RateName
                });

                AuthService.GetInstance().CurrentClub.Balance += Convert.ToDecimal(totalPrice);


                if (_computer.Rent == null)
                {
                    rentsRepository.Add(rent);
                }
                else
                {
                    rentsRepository.Update(_computer.Rent.Id, rent);
                }
                
                Logger.Add($"Создал новую аренду длиной {rent.Length.ToShortTimeString()}");
                SetEditMode(false);
            }
            catch (Exception e)
            {
                NotifyModalWindow.Show(NotifyKind.Error, "Произошла ошибка при изменении деталь аренды");
            }

        }

        [RelayCommand]
        private void CancelChanges() 
        {
            SetEditMode(false);
        }


        private void SetEditMode(bool value = true)
        {
            EditMode = value;
        }

        private string CalculateLeftTime()
        {
            if (_computer.Rent == null)
            {
                return "00:00:00";
            }

            DateTime currentTime = DateTime.Now;
            DateTime endTime = _computer.Rent.StartTime.Add(_computer.Rent.Length.ToTimeSpan());

            if (currentTime > _computer.Rent.StartTime && currentTime < endTime)
            {
                TimeSpan timeLeft = endTime - currentTime;
                return timeLeft.ToString(@"hh\:mm\:ss");
            }


            //rent is over
            RentsRepository repository = RepositoryServiceLocator.Resolve<RentsRepository>();
            Rent oldRent = _computer.Rent;
            _computer.Rent = null;
            repository.Delete(oldRent);

            Logger.Add(oldRent.ToString() + " окончена!");

            UpdateRentTime();
            return "00:00:00";
        }

        private void UpdateRentTime() 
        {
            if(_computer.Rent != null) 
            {
                RentalStartTime = _computer.Rent.StartTime;
                RentDuration = _computer.Rent.Length.ToTimeSpan();
            }
            else 
            {
                RentalStartTime = DateTime.Now;
                RentDuration = new TimeSpan(0);
            }
        }
    }
}
