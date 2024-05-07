﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.Services;
using ComputerClub.ViewModel.modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        private IEnumerable<Club> _selectedClubs;


        


        public AddStaffModalWindowVM() 
        {
            ClubList = RepositoryServiceLocator.Resolve<ClubRepository>().GetAll(); 
        }

        public event EventHandler Done;

        public IEnumerable<Club> ClubList { get; set; }

        public List<string> Roles => AuthService.Roles;

        [RelayCommand]
        private void FormSubmit()
        {
            AuthService auth = AuthService.GetInstance();
            StaffRepository repository = RepositoryServiceLocator.Resolve<StaffRepository>();

            foreach(Club club in SelectedClubs) 
            {
                string hash = auth.GetHash(Password);
                Staff person = new Staff() {Fullname = FullName, Role = Role, PassHash = hash};
                person.Clubs.Add(club);
                repository.Add(person);
            }

            Done?.Invoke(this, EventArgs.Empty);
        }

    }
}