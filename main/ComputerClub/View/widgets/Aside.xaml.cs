using ComputerClub.Services;
using ComputerClub.View.pages;
using ComputerClub.View.shared;
using ComputerClub.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;


namespace ComputerClub.View.widgets
{

    public partial class Aside : UserControl
    {
        public Aside()
        {
            InitializeComponent();
            DataContext = ViewModel = new NavigationVM();
        }

        private NavigationVM ViewModel { get; set; }

        private void NavigateHandler(object sender, RoutedEventArgs args) 
        {
            IconButton btn = (IconButton) ((Button) args.OriginalSource).DataContext;
            try
            {
                switch (btn.Name)
                {
                    case "toHome":
                        NavService.NavigateTo(new HomePage());
                        break;
                    case "toComputers":
                        NavService.NavigateTo(new ComputersPage());
                        break;
                    case "toRates":
                        NavService.NavigateTo(new RatesPage());
                        break;
                    case "toStaff":
                        NavService.NavigateTo(new StaffPage());
                        break;
                    case "toLogs":
                        NavService.NavigateTo(new LogsPage());
                        break;
                    default: throw new ArgumentException();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Navigation Exception: " + ex.Message);
            }
        }

        private void ShowSettingsHandler(object sender, RoutedEventArgs args) 
        {
            ViewModel.OpenSettingsCommand.Execute(null);
        }

    }
}
