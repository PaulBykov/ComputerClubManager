using ComputerClub.Model;
using ComputerClub.Services;
using System.Collections.Generic;
using System.Linq;


namespace ComputerClub.Repositories
{

    public class RentsRepository : IRepository<Rent>
    {
        private ComputerClubContext _context;
        private AuthService _auth = AuthService.GetInstance();

        public RentsRepository(ComputerClubContext context) 
        {
            _context = context;
        }

        public int Count => _context.Rents.Where(r => r.Computer.Club == _auth.CurrentClub).Count();
        public void Add(Rent item)
        {
            _context.Rents.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Rent item)
        {
            _context.Rents.Remove(item);
            _context.SaveChanges();
        }

        public Rent Get(int id)
        {
            return _context.Rents.Where(r => r.Id.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<Rent> GetAll()
        {
            return _context.Rents.ToList();
        }

        public void Update(int id, Rent item)
        {
            Rent oldRent = Get(id);
            
            oldRent.StartTime = item.StartTime;
            oldRent.Length = item.Length;

            _context.SaveChanges();
        }
    }
}
