using ComputerClub.Model;
using ComputerClub.ViewModel;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using ListViewItem = System.Windows.Controls.ListViewItem;


namespace ComputerClub.View.pages
{
    public partial class ComputersPage : Page
    {
        public ComputersPage()
        {
            InitializeComponent();

            DataContext = ViewModel = new ComputersPageVM(ListViewComputers);
        }

        private ComputersPageVM ViewModel { get; set; }


        public void CardClicked(object sender, MouseButtonEventArgs args)
        {
            var listViewItem = sender as ListViewItem;
            var computer = listViewItem?.Content as Computer;

            if (computer != null)
            {
                ViewModel.OpenComputerDetailsCommand.Execute(computer);
            }
        }
    }
}
