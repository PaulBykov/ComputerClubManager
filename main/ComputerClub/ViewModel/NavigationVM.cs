using CommunityToolkit.Mvvm.Input;
using ComputerClub.Services;
using ComputerClub.View.windows;
using System.Windows;
using System.Windows.Controls;



namespace ComputerClub.ViewModel
{
    public partial class NavigationVM
    {
        public NavigationVM() 
        {
            
        }


        [RelayCommand]
        private void OpenSettings()
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            Page parentWindow = ((Frame)(Application.Current.MainWindow.Content)).Content as Page;
            Effector effector = new Effector(parentWindow);

            effector.ApplyBlurEffect(15);

            settingsWindow.ShowDialog();

            effector.ClearEffect();
        }


    }
}
