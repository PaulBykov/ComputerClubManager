using ComputerClub.Model;
using ComputerClub.ViewModel;
using ComputerClub.ViewModel.modal;
using System;
using System.Windows;

namespace ComputerClub.View.windows
{
    public partial class EditRateModalWindow : Window, IModalWindow
    {
        public EditRateModalWindow(Rate rate)
        {
            InitializeComponent();
            DataContext = ViewModel = new EditRateWindowVM(rate);
            ViewModel.Done += Finish;
        }

        private EditRateWindowVM ViewModel { get; set; }

        public void Finish(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
