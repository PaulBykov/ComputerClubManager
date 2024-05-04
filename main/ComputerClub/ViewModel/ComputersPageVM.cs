using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.Services;
using ComputerClub.View.windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;


namespace ComputerClub.ViewModel
{
    public partial class ComputersPageVM : ObservableObject
    {
        [ObservableProperty]
        private IEnumerable<Computer> _computers;

        private ComputersRepository _computersRepository;

        private RentTimer _timer;

        public ComputersPageVM(ListView computerList) 
        {
            _computersRepository = RepositoryServiceLocator.Resolve<ComputersRepository>();
            _timer = new RentTimer(() => computerList.Items.Refresh());

            AuthService.GetInstance().PropertyChanged += AuthServiceChangesHandler;
            _computersRepository.DatabaseChanges += DatabaseChangesHandler;
            UpdateComputersData();
            _timer.Start();
        }


        [RelayCommand]
        public void ShowAddNewComputerWindow()
        {
            AddComputerModalWindow addComputerWindow = new AddComputerModalWindow();

            Effector.TryApplyModalEffects(addComputerWindow);
        }

        [RelayCommand]
        public void OpenComputerDetails(object targetComputer)
        {
            try
            {
                Computer computer = (Computer) targetComputer;
                DetailedComputerModalWindow window = new DetailedComputerModalWindow(computer);

                Effector.TryApplyModalEffects(window);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during opening computer details: " + ex.Message);
            }
        }

        private void AuthServiceChangesHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(AuthService.CurrentClub)) 
            {
                return;
            }

            if (AuthService.GetInstance().CurrentClub == null)
            {
                return;
            }


            UpdateComputersData();
        }

        private void DatabaseChangesHandler(object sender, EventArgs e)
        {
            UpdateComputersData();
        }


        private void UpdateComputersData()
        {
            Computers = _computersRepository.GetAll();
        }
    }
}
