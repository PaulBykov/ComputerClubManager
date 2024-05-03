using ComputerClub.ViewModel;
using System.Windows.Controls;


namespace ComputerClub.View.pages
{
    public partial class RatesPage : Page
    {

        public RatesPage()
        {
            InitializeComponent();

            DataContext = new RatesPageVM();
        }


    }
}
