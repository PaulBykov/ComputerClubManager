using ComputerClub.ViewModel;
using ComputerClub.ViewModel.modal;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using ComputerClub.Model;
using System.Linq;

namespace ComputerClub.View.windows
{
    public partial class AddStaffModalWindow : Window, IModalWindow
    {
        public AddStaffModalWindow()
        {
            InitializeComponent();
            DataContext = ViewModel = new AddStaffModalWindowVM();
            ViewModel.Done += Finish;
        }


        private AddStaffModalWindowVM ViewModel { get; set; }

        public void Finish(object sender, EventArgs e)
        {
            this.DialogResult = true;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passBox = sender as PasswordBox;

            ViewModel.Password = passBox.Password;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectedClubs = ClubLB.SelectedItems.Cast<Club>();
        }
    }
}
