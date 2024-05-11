using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.Services;
using ComputerClub.ViewModel.modal;
using System;
using System.Collections.Generic;

namespace ComputerClub.ViewModel
{
    public partial class AddStaffModalWindowVM : ObservableObject, IModalWindowVM
    {
        [ObservableProperty]
        private string _fullName;

        [ObservableProperty]
        private string _login;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _role;

        [ObservableProperty]
        private bool _clubSectionVisibility = true;

        [ObservableProperty]
        private IEnumerable<Club> _selectedClubs;


        public AddStaffModalWindowVM() 
        {
            ClubList = RepositoryServiceLocator.Resolve<ClubRepository>().GetAll(); 
        }

        public event EventHandler Done;

        public IEnumerable<Club> ClubList { get; private set; }

        public List<string> Roles => AuthService.Roles;


        [RelayCommand]
        private void FormSubmit()
        {
            UserRepository repository = RepositoryServiceLocator.Resolve<UserRepository>();

            string hash = AuthService.GetHash(Password);
            User person = new User()
            {
                Fullname = FullName,
                Role = Role,
                Login = Login,
                PassHash = hash
            };


            foreach (Club club in SelectedClubs) 
            {
                person.Clubs.Add(club);
            }

            repository.Add(person);

            Done?.Invoke(this, EventArgs.Empty);
        }


        partial void OnRoleChanged(string oldValue, string newValue)
        {
            UpdateRoleStatus(newValue);
        }

        private void UpdateRoleStatus(string newRole) 
        {
            ClubSectionVisibility = RoleIdentityService.IsAdmin(newRole);
        }
    }
}
