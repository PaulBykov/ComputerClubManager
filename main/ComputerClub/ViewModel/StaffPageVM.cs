using CommunityToolkit.Mvvm.ComponentModel;
using ComputerClub.Model.Database;
using ComputerClub.Repositories;
using System.Collections.ObjectModel;


namespace ComputerClub.ViewModel
{
    public partial class StaffPageVM : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Staff> _staff;

        public StaffPageVM() 
        {
            _staff = new ObservableCollection<Staff>(
                RepositoryServiceLocator.Resolve<StaffRepository>().GetAllStaff()
            );
        }
    }
}
