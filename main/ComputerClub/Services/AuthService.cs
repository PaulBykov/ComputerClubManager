using ComputerClub.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using ComputerClub.Exceptions;


namespace ComputerClub.Services
{
    public partial class AuthService : ObservableObject
    {
        private static AuthService Instance;

        private DbSet<Club> _clubs;
        private DbSet<Staff> _staff;

        private AuthService(ComputerClubContext context)
        {
            _clubs = context.Clubs;
            _staff = context.Staff;
        }

        [ObservableProperty]
        public Staff _currentUser;

        [ObservableProperty]
        public Club _currentClub;

        
        public static AuthService GetInstance(ComputerClubContext context = null) 
        {
            if (Instance == null)
            {
                if(context == null)
                {
                    throw new AuthException("Ошибка прав доступа: вы не авторизованы");
                }

                Instance = new AuthService(context);
            }

            return Instance;
        }

        private string GetHash(string pass)
        {
            MD5 md5 = MD5.Create();
            byte[] passBytes = Encoding.ASCII.GetBytes(pass);
            byte[] passHash = md5.ComputeHash(passBytes);

            return Convert.ToHexString(passHash);
        }

        public bool TryAuth(string login, string password)
        {
            Club club = _clubs.Where(c => c.ClubLogin == login).FirstOrDefault();

            if (club == null)
            {
                return false;
            }

            string passHash = GetHash(password);
            CurrentUser = _staff.Where(u => u.ClubId == club.Id && u.PassHash == passHash).FirstOrDefault();

            if (CurrentUser == null)
            {
                return false;
            }

            CurrentClub = club;
            return true;
        }

        public void Clear() 
        {
            CurrentUser = null;
            CurrentClub = null;
        }
    }
}
