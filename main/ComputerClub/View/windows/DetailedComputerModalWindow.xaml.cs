using ComputerClub.Model;
using ComputerClub.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;

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
            ViewModel.ShouldSaveChanges += SaveChanges;
        }

        private void Finish(object? sender, EventArgs args)
        {
            DialogResult = true;
        }

        private void SaveChanges(object? sender, EventArgs args) 
        {
            BindingExpression timeBindingExpression = RentTimeField.GetBindingExpression(TimeSpanUpDown.ValueProperty);
            BindingExpression comboboxBindingExpression = RateCombobox.GetBindingExpression(ComboBox.SelectedValueProperty);
            timeBindingExpression.UpdateSource();
            comboboxBindingExpression.UpdateSource();
        }
    }
}
