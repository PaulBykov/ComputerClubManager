﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Helpers;
using ComputerClub.Model;
using ComputerClub.Repositories;
using System;
using System.Collections.Generic;

namespace ComputerClub.ViewModel
{
    public partial class DetailedComputerWindowVM : ObservableObject, IModelWindowVM
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


        private Computer _computer;
        private ComputerNumberConverter _converter = new ComputerNumberConverter();
        private RentTimer _timer;

        public event EventHandler Done;
        public event EventHandler ShouldSaveChanges;

        public DetailedComputerWindowVM(Computer computer)
        {
            _computer = computer;

            if(_computer.Rent != null)
            {
                UpdateRentTime();
            }

            Rates = RepositoryServiceLocator.Resolve<RatesRepository>().GetAll();
            SelectedRate = computer.RateNameNavigation;

            _timer = new RentTimer(() => TimeLeft = CalculateLeftTime());
            _timer.Start();
        }


        public int ComputerNumber { 
            get
            {
                return _converter.GetComputerNumberById(_computer.Id);
            } 
        }
        public bool AntiEditMode => !EditMode;


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
                ComputersRepository repository = RepositoryServiceLocator.Resolve<ComputersRepository>();

                repository.Delete(_computer);
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
            ShouldSaveChanges?.Invoke(this, EventArgs.Empty);

            _computer.RateName = SelectedRate.Name;
            _computer.RateNameNavigation = SelectedRate;

            Rent rent = new Rent() {
                Computer = _computer,
                ComputerId = _computer.Id,
                Length = TimeOnly.FromTimeSpan(RentDuration),
                StartTime=RentalStartTime 
            };
            RentsRepository repository = RepositoryServiceLocator.Resolve<RentsRepository>();


            if(_computer.Rent == null) 
            {
                repository.Add(rent);
            }
            else 
            {
                repository.Update(_computer.Rent.Id, rent);
            }

            SetEditMode(false);
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
            else
            {
                return "00:00:00";
            }
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
