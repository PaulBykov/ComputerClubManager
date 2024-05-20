using System.Windows;
using ComputerClub.Helpers;
using ComputerClub.View;


namespace ComputerClub
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Content = new HomePage();
        }
        
    }
}
