using ComputerClub.View.pages;
using System.Windows;
using ComputerClub.Helpers;


namespace ComputerClub
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

            AppInit.Init();
            MainFrame.Content = new ComputersPage();
        }

        
    }
}
