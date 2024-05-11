using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.Services;
using System;
using System.Text;
using System.Windows;
using ComputerClub.Exceptions;


namespace ComputerClub.Providers
{
    public class SessionStatsProvider
    {
        private readonly AuthService _authService = AuthService.GetInstance();
        private readonly SessionRepository _sessionRepository = RepositoryServiceLocator.Resolve<SessionRepository>();
        
        public SessionStatsProvider() 
        {
            CurrentSession = GetCurrentSessionInfo();
        }


        public Session CurrentSession { get; private set; }

        public bool IsInactiveSession
        {
            get
            {
                return _currentSession == null;
            }
            private set { }
        }  

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
                    Club = _authService.CurrentClub,
                    ClubId = _authService.CurrentClub.Id,
                    UserLogin = _authService.CurrentUser.Login
                };

                _authService.CurrentClub.Session = CurrentSession;
                _sessionRepository.Add(CurrentSession);

                IsInactiveSession = false;
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
