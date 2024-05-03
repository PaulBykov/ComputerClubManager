using ComputerClub.Model;
using ComputerClub.View.pages;
using System.Windows;


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
