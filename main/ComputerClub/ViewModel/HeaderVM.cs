using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Services;
using ComputerClub.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ComputerClub.ViewModel
{
    public partial class HeaderVM : ObservableObject
    {
        [ObservableProperty]
        private ICollection<Button> _userButtons = new List<Button>();

        [ObservableProperty]
        private ICollection<Club> _clubs;

        [ObservableProperty]
        private object _geoIconSelectedItem;

        [ObservableProperty]
        private ICollection<object> _geoIconItemSource;

         [ObservableProperty]
        private ICollection<object> _userIconItemsSource;


        private readonly AuthService _authService = AuthService.GetInstance();


        public HeaderVM() 
        {
            UserButtons.Add(CreateLogOutButton());
            Clubs = GetClubs();

            GeoIconSelectedItem = Clubs.First(c => c.Id.Equals(_authService.CurrentClub.Id));
            GeoIconItemSource   = Clubs.Cast<object>().ToList();
            UserIconItemsSource = UserButtons.Cast<object>().ToList();

            SwitchSelectedClub += ChangeCurrentClub;
        }


        public Action<object> SwitchSelectedClub;


        [RelayCommand]
        private void LogOut()
        {
            var oldWindow = Application.Current.MainWindow;
            var loginWindow = new LoginWindow();

            Application.Current.MainWindow = loginWindow;

            loginWindow.Show();
            oldWindow.Close();
            AuthService.GetInstance().Clear();
        }


        private ICollection<Club> GetClubs() 
        {
            return _authService.GetAvailableClubs();
        }
        private Button CreateLogOutButton()
        {
            var button = new Button
            {
                Content = "Выйти",
                Background = System.Windows.Media.Brushes.Transparent,
                Command = LogOutCommand,
            };

            button.SetResourceReference(Control.ForegroundProperty, "c_text_light");

            return button;
        }
        private void ChangeCurrentClub(object club) 
        {
            try 
            {
                _authService.CurrentClub = (Club)club;
            }
            catch (InvalidCastException e)
            {
                MessageBox.Show("Exception during club changing;\n Got object that isn't club: " + e.Message);
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Exception during club changing: " + ex.Message);  
            }
            
        }
    }
}
