using System.Windows;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Services;

namespace ComputerClub.View.modal
{
    public partial class ConfirmationModalWindow : Window
    {

        public ConfirmationModalWindow(string msg)
        {
            Message = msg;
            InitializeComponent();
        }

        public string Message { get; private set; }

        // returns the DialogResult of itself
        public static bool? Show(string message = "Вы уверены?")
        {
            var window = new ConfirmationModalWindow(message);

            return window.ShowDialog();
        }

        [RelayCommand]
        private void Submit()
        {
            DialogResult = true;
        }
    }
}
