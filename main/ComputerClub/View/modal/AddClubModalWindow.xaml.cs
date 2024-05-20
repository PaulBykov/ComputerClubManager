using System;
using System.Windows;
using ComputerClub.View.widgets;
using ComputerClub.ViewModel;
using ComputerClub.ViewModel.modal;

namespace ComputerClub.View.modal
{
    public partial class AddClubModalWindow : Window, IModalWindow
    {
        public AddClubModalWindow(HeaderVM header)
        {
            InitializeComponent();
            DataContext = ViewModel = new AddClubModalWindowVM(header);
            ViewModel.Done += Finish;
        }

        private AddClubModalWindowVM ViewModel { get; set; }

        public void Finish(object sender, EventArgs e)
        {
            DialogResult = true;
        }
    }
}
