using ComputerClub.Model;
using System.Collections.Generic;
using System.Linq;

namespace ComputerClub.Repositories
{
    public class StaffRepository : IRepository<Staff>
    {
        private ComputerClubContext _context;
        public StaffRepository(ComputerClubContext context)
        {
            _context = context;
        }

        public void Add(Staff item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Staff item)
        {
            throw new System.NotImplementedException();
        }

        public Staff Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Staff> GetAll()
        {
           return _context.Staff.ToList();
        }

        public void Update(Staff item)
        {
            throw new System.NotImplementedException();
        }
    }
}
