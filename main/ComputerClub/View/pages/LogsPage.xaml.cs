using System.Windows.Controls;
using ComputerClub.ViewModel;


namespace ComputerClub.View.pages
{
    public partial class LogsPage : Page
    {
        public LogsPage()
        {
            InitializeComponent();
            DataContext = ViewModel = new LogsPageVM();
        }

        private LogsPageVM ViewModel { get; set; }

    }
}
