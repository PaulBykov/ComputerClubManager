using ComputerClub.ViewModel;
using System.Windows;


namespace ComputerClub.View.windows
{
    public partial class AddComputerModalWindow : Window
    {
        public AddComputerModalWindow()
        {
            InitializeComponent();
            DataContext = new AddComputerWindowVM(this);
        }
    }
}
