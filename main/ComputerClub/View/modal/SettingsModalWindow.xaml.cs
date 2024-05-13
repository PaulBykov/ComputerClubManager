using System;
using System.Windows;
using ComputerClub.ViewModel.modal;


namespace ComputerClub.View.windows
{
    public partial class SettingsWindow : Window, IModalWindow
    {
        public SettingsWindow()
        {
            InitializeComponent();
            DataContext = ViewModel = new SettingsWindowVM();
            ViewModel.Done += Finish;
        }

        private SettingsWindowVM ViewModel { get; set; }
        public void Finish(object sender, EventArgs e)
        {
            DialogResult = true;
        }
    }
}
