using ComputerClub.Model;
using ComputerClub.Services;
using System;
using System.Windows;
using System.Windows.Input;


namespace ComputerClub.View
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AuthService authService = AuthService.GetInstance(new ComputerClubContext());
            string login = txtUser.Text;
            string pass = txtPass.Password;

            if (authService.TryAuth(login,pass)) {
                var main = new MainWindow();
                Application.Current.MainWindow = main;
                main.Show();
                this.Close();
            }
            else
            {
                // wrong pass or login
                MessageBox.Show("unlucku");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
