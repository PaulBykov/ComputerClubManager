using System;
using ComputerClub.Model;
using System.Collections.Generic;
using System.Linq;


namespace ComputerClub.Repositories
{
    public class RatesRepository : IRepository<Rate>
    {
        private ComputerClubContext _context;
        public RatesRepository(ComputerClubContext context)
        {
            _context = context;
        }

        public void Add(Rate item)
        {
            try
            {
                _context.Rates.Add(item);
                _context.SaveChanges();
            }
            catch(Exception e) { }
        }

        public void Delete(Rate item)
        {
            try
            {
                _context.Rates.Remove(item);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
            }
        }

        public Rate Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Rate> GetAll()
        {
            return _context.Rates?.ToList();
        }

        public bool Has(string name) 
        {
            try
            {

                return _context.Rates.Any(r => r.Name.Equals(name));
            }
            catch (Exception e)
            {
            }

            return false;
        }

        public void Update(Rate item)
        {
            _context.Rates.Update(item);
            _context.SaveChanges();
        }
    }
}
