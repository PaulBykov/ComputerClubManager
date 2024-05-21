using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Exceptions;
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.Services;
using ComputerClub.View.modal;
using Microsoft.IdentityModel.Tokens;

namespace ComputerClub.ViewModel.modal
{
    public partial class AddClubModalWindowVM : ObservableValidator, IModalWindowVM
    {
        [ObservableProperty] 
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        private string _clubName;

        private readonly HeaderVM _header;

        public AddClubModalWindowVM(HeaderVM header)
        {
            _header = header;
        }


        public event EventHandler Done;


        [RelayCommand]
        private void AddClub()
        {
            if (!GetErrors(nameof(ClubName)).IsNullOrEmpty())
            {
                MessageBox.Show("Данные не валидны");
                return;
            }

            try
            {
                ClubRepository clubRepository = RepositoryServiceLocator.Resolve<ClubRepository>();
                UserRepository userRepository = RepositoryServiceLocator.Resolve<UserRepository>();
                AuthService auth = AuthService.GetInstance();

                if (clubRepository.Has(ClubName))
                {
                    throw new InvalidDataException("Клуб с таким именем уже существует!");
                }

                Club newClub = new Club 
                {
                    Name = ClubName,
                    Balance = 0
                };

                clubRepository.Add(newClub); 

                var user = userRepository.GetByLogin(auth.CurrentUser.Login);
                user.Clubs.Add(newClub);
                auth.CurrentUser = null;
                userRepository.Update(user);
                auth.CurrentUser = user;


                Logger.Add($"Добавил новый клуб: {newClub}");
                Done?.Invoke(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                NotifyModalWindow.Show(NotifyModalWindow.NotifyKind.Error, ex.Message);
            }
        }
    }
}
