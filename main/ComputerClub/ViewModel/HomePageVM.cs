using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Providers;

namespace ComputerClub.ViewModel
{
    public partial class HomePageVM : ObservableObject
    {
        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private int _freeComputersCount;
        
        [ObservableProperty]
        private int _rentedComputersCount;

        [ObservableProperty]
        private string _sessionBeginTime;


        [ObservableProperty]
        private bool _isInactiveSession;


        private readonly SessionStatsProvider _sessionStatsProvider;
        private readonly ComputerStatsProvider _computerStatsProvider;

        public HomePageVM() 
        {
            _sessionStatsProvider = new SessionStatsProvider();
            _computerStatsProvider = new ComputerStatsProvider();

            IsInactiveSession = _sessionStatsProvider.IsInactiveSession;
            UpdateData();
        }
        

        [RelayCommand]
        public void ActivateSession() 
        {
            _sessionStatsProvider.BeginSession();

            IsInactiveSession = _sessionStatsProvider.IsInactiveSession;
            UpdateData();
        }


        public void UpdateData() 
        {
            if(IsInactiveSession) { return; }

            Session session = _sessionStatsProvider.CurrentSession;

            FreeComputersCount = _computerStatsProvider.GetFreeComputersCount();
            RentedComputersCount = _computerStatsProvider.GetRentedComputersCount();

            UserName = _sessionStatsProvider.GetShortedSessionOwner();
            SessionBeginTime = session..ToString(@"dd/MM/yyyy") + "  " + SessionStatsProvider.SessionBeginTime.TimeOfDay.Hours + ":" + SessionStatsProvider.SessionBeginTime.TimeOfDay.Minutes;
        }

    }
}
