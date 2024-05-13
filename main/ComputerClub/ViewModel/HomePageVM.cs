using System.ComponentModel;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Model;
using ComputerClub.Providers;
using ComputerClub.Services;

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

        private readonly SessionStatsProvider _sessionStatsProvider = new();
        private readonly ComputerStatsProvider _computerStatsProvider  = new();


        public HomePageVM()
        {
            UpdateData();
            AuthService.GetInstance().PropertyChanged += OnPropertyChanged;
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
            Logger.Add($"Начал смену в клубе {_sessionStatsProvider.CurrentSession?.Club.Name}");
            UpdateData();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateData();
        }
        private void UpdateData() 
        {
            IsInactiveSession = _sessionStatsProvider.IsInactiveSession;
            FreeComputersCount = _computerStatsProvider.GetFreeComputersCount();
            RentedComputersCount = _computerStatsProvider.GetRentedComputersCount();

            if (IsInactiveSession) { return; }

            Session session = _sessionStatsProvider.CurrentSession;
            UserName = _sessionStatsProvider.GetShortedSessionOwner();
            SessionBeginTime = session.BeginTime.ToString(@"dd/MM/yyyy HH:mm");
        }
    }
}
