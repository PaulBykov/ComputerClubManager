using CommunityToolkit.Mvvm.Input;
using ComputerClub.Services;
using ComputerClub.View.windows;


namespace ComputerClub.ViewModel
{
    public partial class AsideVM
    {

        public AsideVM()
        {

        }


        public bool IsOwner => RoleIdentityService.IsOwner();


        [RelayCommand]
        private void OpenSettings()
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            
            Effector.TryApplyModalEffects(settingsWindow);
        }


    }
}
