using CommunityToolkit.Mvvm.Input;
using ComputerClub.Services;
using ComputerClub.View.windows;
using ComputerClub.ViewModel;
using System.Windows;
using System.Windows.Controls;


namespace ComputerClub.View.pages
{
    public partial class ComputersPage : Page
    {

        public ComputersPage()
        {
            InitializeComponent();

            DataContext = new ComputersPageVM(ListViewComputers);
        }

        

    }
}
