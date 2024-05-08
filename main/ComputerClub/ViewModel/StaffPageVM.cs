using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.Services;
using ComputerClub.View.windows;
using System.Collections.Generic;


namespace ComputerClub.ViewModel
{
    public partial class StaffPageVM : ObservableObject
    {
        [ObservableProperty]
        private IEnumerable<User> _staff;

        public StaffPageVM() 
        {
            _staff = RepositoryServiceLocator.Resolve<UserRepository>().GetAll();
        }


        [RelayCommand]
        private void ShowAddNewStaffWindow() 
        {
            AddStaffModalWindow window = new AddStaffModalWindow();
            Effector.TryApplyModalEffects(window);
        }
    }
}
