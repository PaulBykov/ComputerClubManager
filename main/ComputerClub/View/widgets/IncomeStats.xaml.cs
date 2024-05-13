using System.Windows.Controls;
using ComputerClub.ViewModel;


namespace ComputerClub.View.widgets
{

    public partial class IncomeStats : UserControl
    {
        public IncomeStats()
        {
            InitializeComponent();
            DataContext = ViewModel = new IncomeStatsVM();
        }

        private IncomeStatsVM ViewModel { get; set; }

    }
}
