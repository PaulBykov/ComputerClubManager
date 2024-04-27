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

        private void SubmitButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var main = new MainWindow();
            Application.Current.MainWindow = main;
            main.Show();
            this.Close();
        }
    }
}
