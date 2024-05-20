using System;
using System.Windows;
using ComputerClub.ViewModel;
using ComputerClub.ViewModel.modal;


namespace ComputerClub.View.modal
{
    public partial class DeleteClubModalWindow : Window, IModalWindow
    {
        public DeleteClubModalWindow(HeaderVM headerVM)
        {
            InitializeComponent();
            DataContext = ViewModel = new DeleteClubModalWindowVM(headerVM);

            ViewModel.Done += Finish;
        }

        private DeleteClubModalWindowVM ViewModel { get; set; }

        public void Finish(object sender, EventArgs e)
        {
            DialogResult = true;
        }
    }
}
