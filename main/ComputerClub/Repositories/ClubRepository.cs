using ComputerClub.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputerClub.Repositories
{
    public class ClubRepository : IRepository<Club>
    {
        private ComputerClubContext _context;
        public ClubRepository(ComputerClubContext context)
        {
            _context = context;
        }

        public IEnumerable<Club> GetAll()
        {
            return _context.Clubs.ToList();
        }


        public void Add(Club item)
        {
            _context.Clubs.Add(item);
        }

        public void Delete(Club item)
        {
            _context.Clubs.Remove(item);
        }

        public Club Get(int id)
        {
            return _context.Clubs.Where(c => c.Id ==  id).FirstOrDefault();
        }

        public void Update(Club item)
        {
            throw new NotImplementedException();
        }
    }
}
