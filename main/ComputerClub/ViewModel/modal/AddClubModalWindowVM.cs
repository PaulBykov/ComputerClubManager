using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Exceptions;
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.Services;
using ComputerClub.View.modal;

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
                auth.CurrentUser.Clubs.Add(newClub);
                userRepository.Update(auth.CurrentUser);


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
