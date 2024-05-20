using System.ComponentModel.DataAnnotations;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Services;
using ComputerClub.View;
using Microsoft.IdentityModel.Tokens;

namespace ComputerClub.ViewModel
{
    public partial class LoginWindowVM : ObservableValidator
    {
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(50, ErrorMessage = "Максимальная длина 50 символов")]
        private string _username;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(50, ErrorMessage = "Максимальная длина 50 символов")]
        private string _password;


        [ObservableProperty] 
        public string _passErrors;


        public LoginWindowVM(LoginWindow view)
        {
            View = view;
        }


        private LoginWindow View { get; set; }



        [RelayCommand]
        private void Login()
        {
            if (!GetPassErrors().IsNullOrEmpty() || !GetErrors(nameof(Username)).IsNullOrEmpty())
            {
                MessageBox.Show("Данные не валидны");
                return;
            }


            AuthService authService = AuthService.GetInstance(new ComputerClubContext());

            if (authService.TryAuth(Username, Password)) {
                var main = new MainWindow();
                Application.Current.MainWindow = main;
                main.Show();
                View.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }


        [RelayCommand]
        private void Close()
        {
            View.Close();
        }

        [RelayCommand]
        private void Minimize()
        { 
            View.WindowState = WindowState.Minimized;
        }

        public string GetPassErrors()
        {
            return string.Join('\n', GetErrors(nameof(Password)));
        }
    }
}
