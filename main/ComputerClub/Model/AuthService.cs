using ComputerClub.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace ComputerClub.Model
{
    public class AuthService
    {
        private DbSet<Club> _clubs;
        private DbSet<Staff> _staff;
        private static Staff _currentUser = null;


        public AuthService(ComputerClubContext context) 
        {
            _clubs = context.Clubs;
            _staff = context.Staff;
        }

        public static Staff CurrentUser { get => _currentUser; }


        private string getHash(string pass) 
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

            string passHash = getHash(password);
           _currentUser = _staff.Where(u => u.ClubId == club.Id && u.PassHash == passHash).FirstOrDefault();
           
            if(CurrentUser == null)
            {
                return false;
            }
            
            return true;
        }
    }
}
