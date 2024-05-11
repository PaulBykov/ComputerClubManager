#nullable enable
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.Services;
using System;
using System.Text;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using ComputerClub.Exceptions;
using Microsoft.VisualBasic;


namespace ComputerClub.Providers
{
    public partial class SessionStatsProvider : ObservableObject
    {
        private readonly AuthService _authService = AuthService.GetInstance();
        private readonly SessionRepository _sessionRepository = RepositoryServiceLocator.Resolve<SessionRepository>();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsInactiveSession))]
        private Session? _currentSession = null;

        public SessionStatsProvider() 
        {
            _currentSession = GetCurrentSessionInfo();

            Application.Current.Exit += Application_Exit;
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (CurrentSession != null)
            {
                _sessionRepository.Delete(CurrentSession);
                CurrentSession = null;
            }
        }

        public bool IsInactiveSession => CurrentSession == null;
        

        private Session GetCurrentSessionInfo()
        {
            return _authService.CurrentClub.Session;
        }

        public void BeginSession()
        {
            try
            {
                if(CurrentSession != null)
                {
                    throw new SessionException("Session for this club is already started!");
                }

                CurrentSession = new Session 
                {
                    ClubId = _authService.CurrentClub.Id,
                    UserLogin = _authService.CurrentUser.Login,
                    BeginTime = DateAndTime.Now
                };


                _sessionRepository.Add(CurrentSession);
                _authService.CurrentClub.Session = CurrentSession;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error during Starting Session; \n" + e.Message);
            }
        }

        public string GetShortedSessionOwner()
        {
            try
            {
                string[] names = CurrentSession.UserLoginNavigation.Fullname.Split(" ");

                if (names.Length < 3)
                {
                    return _authService.CurrentUser.Fullname;
                }


                StringBuilder shortedName = new StringBuilder(names[0] + " ");
                for (byte i = 1; i < names.Length; i++)
                {
                    shortedName.Append(names[i][0] + ". ");
                }

                return shortedName.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
