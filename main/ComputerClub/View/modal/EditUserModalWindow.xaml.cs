using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ComputerClub.Model;
using ComputerClub.ViewModel.modal;

namespace ComputerClub.View.modal
{
    public partial class EditUserModalWindow : Window, IModalWindow
    {
        public EditUserModalWindow(User user)
        {
            InitializeComponent();
            DataContext = ViewModel = new EditUserModalWindowVM(user);
            ViewModel.Done += Finish;
        }

        private EditUserModalWindowVM ViewModel { get; set; }

        public void Finish(object sender, EventArgs e)
        {
            this.DialogResult = true;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectedClubs = ClubLB.SelectedItems.Cast<Club>();
        }
    }
}
