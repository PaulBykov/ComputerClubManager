using ComputerClub.ViewModel;
using System.Windows.Controls;


namespace ComputerClub.View.pages
{

    public partial class StaffPage : Page
    {
        public StaffPage()
        {
            InitializeComponent();
            DataContext = new StaffPageVM();
        }

    }
}
