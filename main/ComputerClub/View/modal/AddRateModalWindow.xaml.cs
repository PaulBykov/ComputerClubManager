using ComputerClub.ViewModel;
using ComputerClub.ViewModel.modal;
using System;
using System.Windows;


namespace ComputerClub.View.windows
{
    public partial class AddRateModalWindow : Window, IModalWindow
    {

        public AddRateModalWindow()
        {
            InitializeComponent();
            DataContext = ViewModel = new AddRateWindowVM();
            ViewModel.Done += Finish;
        }

        
        private AddRateWindowVM ViewModel { get; set; }

        public void Finish(object sender, EventArgs e) 
        {
            this.DialogResult = true;
        }
    }
}
