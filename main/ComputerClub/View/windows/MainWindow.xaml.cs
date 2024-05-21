using System;
using System.Windows;
using ComputerClub.View;


namespace ComputerClub
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                MainFrame.Content = new HomePage();
            }
            catch (Exception e)
            {

            }
        }
        
    }
}
