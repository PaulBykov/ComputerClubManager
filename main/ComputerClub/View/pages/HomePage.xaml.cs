using ComputerClub.Services;
using ComputerClub.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ComputerClub.View.pages;


namespace ComputerClub.View
{

    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            DataContext = ViewModel = new HomePageVM();
            AuthService.GetInstance().PropertyChanged += HandlePropertyChanges;
        }

        private HomePageVM ViewModel { get; set; }

        private void HandlePropertyChanges(object sender, PropertyChangedEventArgs e)
        {
            NavService.NavigateTo(new HomePage());
        }
    }
}
