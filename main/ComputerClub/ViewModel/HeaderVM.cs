using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Services;
using ComputerClub.View;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ComputerClub.ViewModel
{
    public partial class HeaderVM : ObservableObject
    {
        [ObservableProperty]
        private Button[] _userButtons = new Button[1];

        [ObservableProperty]
        private Club[] _clubs;

        private bool _isFirstLoadComplete = false;

        public HeaderVM() 
        {
            UserButtons[0] = CreateLogOutButton();
            _clubs = GetClubs();
            SwitchSelectedClub += ChangeCurrentClub;
        }

        public Action<object> SwitchSelectedClub;

        
        [RelayCommand]
        private void LogOut()
        {
            AuthService.GetInstance().Clear();

            var oldWindow = Application.Current.MainWindow;
            var loginWindow = new LoginWindow();
            Application.Current.MainWindow = loginWindow;
            loginWindow.Show();
            oldWindow.Close();
        }


        private Club[] GetClubs() 
        {
            AuthService auth = AuthService.GetInstance();

            return auth.GetAvalibleClubs().ToArray();
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
                AuthService.GetInstance().CurrentClub = (Club)club;

                if (_isFirstLoadComplete)
                {
                    NavService.Refresh();
                }

                _isFirstLoadComplete = true;
            }
            catch (InvalidCastException e)
            {
                MessageBox.Show("Exception during club changing;\nGetted object is't club: " + e.Message);
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Exception during club changing: " + ex.Message);  
            }
            
        }
    }
}
