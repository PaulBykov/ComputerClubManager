using ComputerClub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ComputerClub.Repositories
{
    public class SessionRepository : IRepository<Session>
    {
        private readonly ComputerClubContext _context;

        public SessionRepository(ComputerClubContext context) 
        {
            _context = context;
        }


        public void Add(Session item)
        {
            try
            {
                _context.Sessions.Add(item);
                _context.SaveChanges();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Delete(Session item)
        {
            try
            {
                _context.Sessions.Remove(Get(item.ClubId));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Session Get(int clubId)
        {
            return _context.Sessions.FirstOrDefault(s => s.ClubId.Equals(clubId));
        }

        public IEnumerable<Session> GetAll()
        {
            return _context.Sessions.ToList();
        }
    }
}
