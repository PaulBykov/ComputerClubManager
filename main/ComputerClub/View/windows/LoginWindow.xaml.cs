using System.Windows;
using ComputerClub.Helpers;
using ComputerClub.ViewModel;


namespace ComputerClub.View
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            
            AppInit.Init();
            DataContext = ViewModel = new LoginWindowVM(this);
        }

        private LoginWindowVM ViewModel { get; set; }

        private void TxtPass_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (ViewModel == null)
            {
                return;
            }

            ViewModel.Password = TxtPass.Password;
            ViewModel.PassErrors = ViewModel.GetPassErrors();
        }
    }
}
