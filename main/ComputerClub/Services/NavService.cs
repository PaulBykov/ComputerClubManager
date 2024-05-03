using ComputerClub.View;
using ComputerClub.View.pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ComputerClub.Services
{
    public static class NavService
    {
        private static NavigationService NavigationService { get; } = (Application.Current.MainWindow as MainWindow).MainFrame.NavigationService;

        public static void NavigateTo(Page page)
        {
            NavigationService?.Navigate(page);
        }

        public static void Refresh() 
        {
            NavigationService?.Refresh();
        }

    }
}
