using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Helpers;
using ComputerClub.Model;
using ComputerClub.Repositories;
using System;
using System.Collections.Generic;
using ComputerClub.ViewModel;

namespace ComputerClub.ViewModel
{
    public partial class DetailedComputerWindowVM : ObservableObject, IModelWindowVM
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(AntiEditMode))]
        private bool _editMode = false;

        [ObservableProperty]
        private DateTime _rentalStartTime = new DateTime();

        [ObservableProperty]
        private TimeSpan _rentDuration = new TimeSpan();

        [ObservableProperty]
        private string _rateName;

        [ObservableProperty]
        private string _timeLeft = "00:00:00";

        [ObservableProperty]
        private IEnumerable<Rate> _rates;

        private Computer _computer;

        private ComputerNumberConverter _converter = new ComputerNumberConverter();

        private RentTimer _timer;

        public event EventHandler Done;

        public DetailedComputerWindowVM(Computer computer)
        {
            _computer = computer;

            if(_computer.Rent != null)
            {
                RentalStartTime = _computer.Rent.StartTime;
                RentDuration = _computer.Rent.Length.ToTimeSpan();
            }

            _rates = RepositoryServiceLocator.Resolve<RatesRepository>().GetAll();
            RateName = computer.RateName;

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
        private void SetEditMode() 
        {
            EditMode = true;
        }
        private void UnSetEditMode() 
        {
            EditMode = false;
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
            UnSetEditMode();
        }

        [RelayCommand]
        private void CancelChanges() 
        {
            UnSetEditMode();
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
    }
}
