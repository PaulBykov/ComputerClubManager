using ComputerClub.ViewModel;
using System.Windows.Controls;


namespace ComputerClub.View
{

    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            DataContext = ViewModel = new HomePageVM();
        }

        private HomePageVM ViewModel { get; set; }

    }
}
