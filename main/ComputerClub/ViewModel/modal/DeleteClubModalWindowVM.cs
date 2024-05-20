using System;
using System.Collections.Generic;
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
    public partial class DeleteClubModalWindowVM : ObservableValidator, IModalWindowVM
    {
        [ObservableProperty] 
        private IEnumerable<Club> _clubList;

        [ObservableProperty] 
        [NotifyDataErrorInfo]
        [Required]
        private Club _selectedClub;


        private readonly ClubRepository _clubRepository;
        private readonly HeaderVM _headerVM;


        public DeleteClubModalWindowVM(HeaderVM headerVM)
        {
            _clubRepository = RepositoryServiceLocator.Resolve<ClubRepository>();
            _headerVM = headerVM;

            ClubList = AuthService.GetInstance().GetAvailableClubs();
        }

        
        public event EventHandler Done;


        [RelayCommand]
        private void DeleteClub()
        {
            try
            {
                AuthService auth = AuthService.GetInstance();
                if (ConfirmationModalWindow.Show(
                        $"Вы действительно хотите удалить клуб {SelectedClub}?" +
                        $" Это действие нельзя отменить") == true)
                {
                    
                    if (auth.CurrentClub.Id == SelectedClub.Id)
                    {
                        throw new InvalidDataException("Попытка удалить текущий клуб");
                    }

                    try
                    {
                        auth.CurrentUser.Clubs.Remove(SelectedClub);
                    }
                    catch(Exception e){}
                    
                    _clubRepository.Delete(SelectedClub);
                }

                
                Logger.Add($"Удалил клуб: {SelectedClub}");
                Done?.Invoke(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                NotifyModalWindow.Show(NotifyModalWindow.NotifyKind.Error, ex.Message);
            }
        }

    }
}
