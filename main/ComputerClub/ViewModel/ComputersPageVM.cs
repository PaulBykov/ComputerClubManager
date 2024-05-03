using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Model.Database;
using ComputerClub.Repositories;
using ComputerClub.Services;
using ComputerClub.View.windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;



namespace ComputerClub.ViewModel
{
    public partial class ComputersPageVM : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Computer> _computers;

        private ComputersRepository ComputersRepository { get; set; }


        public ComputersPageVM(ListView computerList) 
        {
            this.ComputersRepository = RepositoryServiceLocator.Resolve<ComputersRepository>();
            this._computers = GetComputers();
            new ComputerTimer(() => computerList.Items.Refresh()).Start();

            AuthService.GetInstance().PropertyChanged += AuthService_PropertyChanged;
        }

        private void AuthService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (AuthService.GetInstance().CurrentClub == null)
            {
                return;
            }

            if (e.PropertyName == nameof(AuthService.CurrentClub))
            {
                Computers = GetComputers();
            }
        }
        
        private ObservableCollection<Computer> GetComputers()
        {
            return new ObservableCollection<Computer>(ComputersRepository.GetAllComputers());
        }

        [RelayCommand]
        public void ShowAddNewComputerWindow()
        {
            AddComputerModalWindow addComputerWindow = new AddComputerModalWindow();
            Page parentWindow = ((Frame)(Application.Current.MainWindow.Content)).Content as Page;
            Effector effector = new Effector(parentWindow);

            effector.ApplyBlurEffect(15);

            addComputerWindow.ShowDialog();

            effector.ClearEffect();
        }
    }
}
