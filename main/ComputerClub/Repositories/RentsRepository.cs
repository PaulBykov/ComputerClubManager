using ComputerClub.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ComputerClub.Repositories
{

    public class RentsRepository : IRepository<Rent>
    {
        private ComputerClubContext _context;

        public RentsRepository(ComputerClubContext context) 
        {
            _context = context;
        }


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
