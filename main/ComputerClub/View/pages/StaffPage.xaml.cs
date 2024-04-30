using ComputerClub.Model.Database;
using System.Collections.ObjectModel;
using System.Windows.Controls;


namespace ComputerClub.View.pages
{

    public partial class StaffPage : Page
    {

        public StaffPage()
        {
            InitializeComponent();
            DataContext = this;

            Staff = new ObservableCollection<Staff>(new ComputerClubContext().Staff);
        }

        public ObservableCollection<Staff> Staff { get; set; }
        
    }
}
