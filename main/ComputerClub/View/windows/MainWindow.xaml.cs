using ComputerClub.View;
using ComputerClub.View.pages;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ComputerClub
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Content = new ComputersPage();
        }


    }
}
