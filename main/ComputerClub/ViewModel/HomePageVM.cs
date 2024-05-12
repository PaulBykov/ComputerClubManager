using System.Windows.Media;
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
        [NotifyPropertyChangedFor(nameof(OpacityColor))]
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

        public Color OpacityColor
        {
            get
            {
                if (_sessionStatsProvider.IsInactiveSession == false)
                {
                    return Color.FromArgb(255, 0, 0, 0);
                }

                return Color.FromArgb(176, 0, 0, 100);
            }
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
            FreeComputersCount = _computerStatsProvider.GetFreeComputersCount();
            RentedComputersCount = _computerStatsProvider.GetRentedComputersCount();

            if (IsInactiveSession) { return; }

            Session session = _sessionStatsProvider.CurrentSession;
            UserName = _sessionStatsProvider.GetShortedSessionOwner();
            SessionBeginTime = session.BeginTime.ToString(@"dd/MM/yyyy HH:mm");
        }

    }
}
