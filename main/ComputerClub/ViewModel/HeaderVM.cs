using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Services;
using ComputerClub.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ComputerClub.View.modal;


namespace ComputerClub.ViewModel
{
    public partial class HeaderVM : ObservableObject
    {
        [ObservableProperty]
        private ICollection<Button> _userButtons = new List<Button>();

        [ObservableProperty]
        private ObservableCollection<Club> _clubs;

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
            Clubs = new ObservableCollection<Club>(GetClubs());


            GeoIconSelectedItem = Clubs.First(c => c.Id.Equals(_authService.CurrentClub.Id));
            GeoIconItemSource   = Clubs.Cast<object>().ToList();
            UserIconItemsSource = UserButtons.Cast<object>().ToList();

            SwitchSelectedClub += ChangeCurrentClub;
        }


        public Action<object> SwitchSelectedClub;

        public bool IsOwner => RoleIdentityService.IsOwner();


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

        [RelayCommand]
        private void OpenAddNewClub()
        {
            var window = new AddClubModalWindow(this);

            Effector.TryApplyModalEffects(window);
        }

        [RelayCommand]
        private void OpenDeleteClub()
        {
            var window = new DeleteClubModalWindow(this);

            Effector.TryApplyModalEffects(window);
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
