using ComputerClub.Model;
using ComputerClub.ViewModel;
using System;
using System.Windows;

namespace ComputerClub.View.windows
{
    public partial class DetailedComputerModalWindow : Window, IModalWindow
    {
        private DetailedComputerWindowVM ViewModel { get; set; }
        public DetailedComputerModalWindow(Computer computer)
        {
            InitializeComponent();
            DataContext = ViewModel = new DetailedComputerWindowVM(computer);
            ViewModel.Done += Finish;
        }

        private void Finish(object? sender, EventArgs args)
        {
            DialogResult = true;
        }
    }
}
