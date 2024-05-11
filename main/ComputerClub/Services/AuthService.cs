using ComputerClub.Model;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using ComputerClub.Exceptions;
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
            _context.Users.Load();
        }

        [ObservableProperty]
        public User _currentUser;

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
            var temp = _context.Users.Include(s => s.Clubs).ToList();
            var temp1 = _context.Clubs.Include(c => c.Users).ToList();

            User user = _context.Users.Where(u => u.Login == login).FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            string passHash = GetHash(password);

            if(user.PassHash.Equals(passHash)) 
            {
                return false;
            }

            // данные совпали

            CurrentUser = user;
            CurrentClub = user.Clubs.First();
            return true;
        }

        public IEnumerable<Club> GetAvalibleClubs() 
        {
            return CurrentUser.Clubs;
        }
        public void Clear() 
        {
            CurrentUser = null;
            CurrentClub = null;
        }
    }
}
