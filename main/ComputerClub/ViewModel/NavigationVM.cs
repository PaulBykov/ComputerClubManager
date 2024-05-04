﻿using CommunityToolkit.Mvvm.Input;
using ComputerClub.Services;
using ComputerClub.View.windows;


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
            
            Effector.TryApplyModalEffects(settingsWindow);
        }


    }
}
