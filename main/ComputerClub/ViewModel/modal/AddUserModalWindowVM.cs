using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using ComputerClub.Exceptions;
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.Services;
using ComputerClub.View.modal;
using static ComputerClub.View.modal.NotifyModalWindow;


namespace ComputerClub.ViewModel.modal
{
    public partial class AddStaffModalWindowVM : ObservableValidator, IModalWindowVM
    {
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required (ErrorMessage = "Это обязательное поле")]
        [MinLength(3, ErrorMessage = "Минимальная длина 3")]
        [MaxLength(50, ErrorMessage = "Максимальная длина 50")]
        private string _fullName;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required (ErrorMessage = "Это обязательное поле")]
        [MinLength(3, ErrorMessage = "Минимальная длина 3")]
        [MaxLength(50, ErrorMessage = "Максимальная длина 50")]
        private string _login;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required (ErrorMessage = "Это обязательное поле")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-zA-Z]).*$", ErrorMessage = "Хотя бы 1 цифра и буква обязательны")]
        [MinLength(8, ErrorMessage = "Минимальная длина 8")]
        [MaxLength(50, ErrorMessage = "Максимальная длина 50")]
        private string _password;

        [ObservableProperty]
        private string _role = "Admin";

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

        public List<string> Roles => [nameof(AuthService.Roles.Admin), nameof(AuthService.Roles.Owner)];


        [RelayCommand]
        private void FormSubmit()
        {
            try
            {
                UserRepository repository = RepositoryServiceLocator.Resolve<UserRepository>();

                if (repository.Has(Login))
                {
                    throw new InvalidDataException("Пользователь с там логином уже существует");
                }

                if (SelectedClubs.ToList().Count < 1)
                {
                    throw new InvalidDataException("Выберите хотя бы один клуб");
                }

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
                Logger.Add($"Добавил нового пользователя - {person.Fullname}");
                NotifyModalWindow.Show(NotifyKind.Success, $"Вы успешно добавили нового пользователя");
            }
            catch (InvalidDataException e)
            {
                NotifyModalWindow.Show(NotifyKind.Error, e.Message);
            }
            catch (Exception e)
            {
                NotifyModalWindow.Show(NotifyKind.Error, "Ошибка при добавлении нового пользователя");
            }
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
