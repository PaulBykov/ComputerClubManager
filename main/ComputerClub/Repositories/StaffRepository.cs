using ComputerClub.Model.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ComputerClub.Repositories
{
    public class StaffRepository : IRepository<Staff>
    {
        private DbSet<Staff> _context;
        public StaffRepository(DbSet<Staff> context)
        {
            _context = context;
        }

        public ICollection<Staff> GetAllStaff()
        {
            return _context.ToList();
        }

        public void Create(Staff item)
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

        public Staff Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Staff item)
        {
            throw new System.NotImplementedException();
        }
    }
}
