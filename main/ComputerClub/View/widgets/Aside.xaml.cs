using ComputerClub.View.pages;
using ComputerClub.View.shared;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace ComputerClub.View.widgets
{

    public partial class Aside : UserControl
    {
        
        public Aside()
        {
            InitializeComponent();
            DataContext = this;
        }


        private void NavigateHandler(object sender, RoutedEventArgs args) 
        {
            IconButton btn = (IconButton) ((Button) args.OriginalSource).DataContext;
            try
            {
                switch (btn.Name)
                {
                    case "toHome":
                        NavigateTo(new HomePage());
                        break;
                    case "toComputers":
                        NavigateTo(new ComputersPage());
                        break;
                    case "toRates":
                        NavigateTo(new RatesPage());
                        break;
                    case "toStaff":
                        NavigateTo(new StaffPage());
                        break;
                    case "toLogs":
                        NavigateTo(new LogsPage());
                        break;
                    default: throw new ArgumentException();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Navigation Exception: " + ex.Message);
            }
        }

        private void NavigateTo(Page page) 
        {
            NavigationService.GetNavigationService(this).Navigate(page);
        }

    }
}
