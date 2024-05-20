using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.Services;
using ComputerClub.View.windows;
using System.Collections.Generic;
using System.Linq;
using ABI.Windows.Devices.Sensors;
using ComputerClub.View.modal;


namespace ComputerClub.ViewModel
{
    public partial class StaffPageVM : ObservableObject
    {
        [ObservableProperty]
        private IEnumerable<User> _staff;

        public StaffPageVM() 
        {
            _staff = RepositoryServiceLocator.Resolve<UserRepository>().GetAll().OrderByDescending(u => u.Role);
        }


        [RelayCommand]
        private void ShowAddNewStaffWindow() 
        {
            AddStaffModalWindow window = new AddStaffModalWindow();
            Effector.TryApplyModalEffects(window);
        }

        [RelayCommand]
        private void ShowEditUserWindow(object user)
        {
            User selectedUser = user as User;
            EditUserModalWindow window = new EditUserModalWindow(selectedUser);
            Effector.TryApplyModalEffects(window);
        }


        [RelayCommand]
        private void RemoveUser(object user)
        {
            try
            {
                User selectedUser = user as User;
                UserRepository repository = RepositoryServiceLocator.Resolve<UserRepository>();

                repository.Delete(selectedUser);
            }
            catch (Exception e)
            {
                NotifyModalWindow.Show(NotifyModalWindow.NotifyKind.Error, "Ошибка удаления пользователя");
            }
        }
    }
}
