using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.Services;
using ComputerClub.View.windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
            Page parentWindow = ((Frame)(Application.Current.MainWindow.Content)).Content as Page;
            Effector effector = new Effector(parentWindow);

            effector.ApplyBlurEffect(15);

            addComputerWindow.ShowDialog();

            effector.ClearEffect();
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
