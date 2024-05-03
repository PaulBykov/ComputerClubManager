using ComputerClub.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputerClub.Repositories
{
    public class ClubRepository : IRepository<Club>
    {
        private DbSet<Club> _context;
        public ClubRepository(DbSet<Club> context)
        {
            _context = context;
        }

        public ICollection<Club> GetAllClubs() 
        {
            return _context.ToList();
        }

        public void Create(Club item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Club Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Club item)
        {
            throw new NotImplementedException();
        }
    }
}
