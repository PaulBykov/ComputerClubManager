using System;
using System.Collections.Generic;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.View.modal;
using static ComputerClub.View.modal.NotifyModalWindow;

namespace ComputerClub.ViewModel
{
    public partial class LogsPageVM : ObservableObject
    {
        [ObservableProperty] 
        private LinkedList<string> _messagesSource = Logger.Messages;

        public LogsPageVM()
        {

        }


        [RelayCommand]
        private void SaveLogsToFile()
        {
            try
            {
                string path = Properties.Settings.Default.LogsPath.TrimEnd();
                string fileName = $"logs_{DateTime.Now}";
                string filePath = Path.ChangeExtension(path + fileName, ".log");

                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (string message in MessagesSource)
                    {
                        writer.WriteLine(message);
                    }

                    
                    NotifyModalWindow.Show(NotifyKind.Success, $"Успешно сохранено в {filePath}");
                }
            }
            catch (Exception ex)
            {
                NotifyModalWindow.Show(NotifyKind.Error, ex.Message);
            }
        }
    }
}
