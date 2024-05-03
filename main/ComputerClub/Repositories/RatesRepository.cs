using ComputerClub.Model.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace ComputerClub.Repositories
{
    public class RatesRepository : IRepository<Rate>
    {
        private DbSet<Rate> _context;
        public RatesRepository(DbSet<Rate> context)
        {
            _context = context;
        }
        
        public ICollection<Rate> GetAllRates() 
        {
            return _context.ToList();            
        }

        public void Create(Rate item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public Rate Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Rate item)
        {
            throw new System.NotImplementedException();
        }
    }
}
