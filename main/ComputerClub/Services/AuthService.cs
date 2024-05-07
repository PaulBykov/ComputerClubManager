using ComputerClub.Model;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using ComputerClub.Exceptions;
using System.Windows.Documents;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace ComputerClub.Services
{
    public partial class AuthService : ObservableObject
    {
        private static AuthService Instance;

        private ComputerClubContext _context;

        public static List<string> Roles = new List<string>() {"admin", "owner"};

        private AuthService(ComputerClubContext context)
        {
            _context = context;

            _context.Clubs.Load();
            _context.Staff.Load();
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

        public string GetHash(string pass)
        {
            MD5 md5 = MD5.Create();
            byte[] passBytes = Encoding.ASCII.GetBytes(pass);
            byte[] passHash = md5.ComputeHash(passBytes);

            return Convert.ToHexString(passHash);
        }

        public bool TryAuth(string login, string password)
        {
            _context.Staff.Load();
            _context.Clubs.Load();
            Club club = _context.Clubs.Where(c => c.ClubLogin == login).FirstOrDefault();

            if (club == null)
            {
                return false;
            }

            string passHash = GetHash(password);

            CurrentUser = _context.Staff
                                        .Where(u => u.Clubs.Any(c => c.Id.Equals(club.Id)) 
                                                    && u.PassHash == passHash)
                                        .FirstOrDefault();

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
