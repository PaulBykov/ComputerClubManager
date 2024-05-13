using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Forms;
using ComputerClub.Properties;
using ComputerClub.View.modal;
using static ComputerClub.View.modal.NotifyModalWindow;

namespace ComputerClub.ViewModel.modal
{
    public partial class SettingsWindowVM : ObservableObject, IModalWindowVM
    {
        [ObservableProperty] 
        private string _logsSelectedPath = Properties.Settings.Default.LogsPath;

        [ObservableProperty] 
        private string _reportsSelectedPath = Properties.Settings.Default.ReportsPath;


        public SettingsWindowVM(){}

        
        public event EventHandler Done;


        [RelayCommand]
        private void OpenLogsPath()
        {
            try
            {
                LogsSelectedPath = SelectPath();
            }
            catch (Exception e)
            {
                NotifyModalWindow.Show(NotifyKind.Error, $"Произошла при выборе пути: " + e.Message);
            }
        }

        
        [RelayCommand]
        private void OpenReportsPath()
        {
            try
            {
                ReportsSelectedPath = SelectPath();
            }
            catch (Exception e)
            {
                NotifyModalWindow.Show(NotifyKind.Error, $"Произошла при выборе пути: " + e.Message);
            }
        }


        [RelayCommand]
        private void OverridePaths()
        {
            try
            {
                Settings.Default.LogsPath = LogsSelectedPath;
                Settings.Default.ReportsPath = ReportsSelectedPath;
                Settings.Default.Save();
                
                Done?.Invoke(null, EventArgs.Empty);
            } 
            catch (Exception e)
            {
                NotifyModalWindow.Show(NotifyKind.Error, $"Ошибка доступа к свойствам проекта: " + e.Message);
            }
        }


        private string SelectPath()
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();
            if (browserDialog.ShowDialog() == DialogResult.OK)
            {
                return browserDialog.SelectedPath + "\\";
            }

            return null;
        }

    }
}
