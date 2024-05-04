using ComputerClub.ViewModel;
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
