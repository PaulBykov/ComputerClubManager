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
        public enum Roles {Admin, Owner}
        private static AuthService _instance;
        private readonly ComputerClubContext _context;


        [ObservableProperty]
        private User _currentUser;

        [ObservableProperty]
        private Club _currentClub;


        private AuthService(ComputerClubContext context)
        {
            _context = context;

            _context.Sessions.Load();
            _context.Clubs.Load();
            _context.Users.Load();
        }

        
        public static AuthService GetInstance(ComputerClubContext context = null) 
        {
            if (_instance == null)
            {
                if(context == null)
                {
                    throw new AuthException("Ошибка прав доступа: контекста не существует");
                }

                _instance = new AuthService(context);
            }

            return _instance;
        }

        public static string GetHash(string pass)
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

        public ICollection<Club> GetAvailableClubs()
        {
            return CurrentUser?.Clubs;
        }
        public void Clear() 
        {
            CurrentUser = null;
            CurrentClub = null;
        }
    }
}
