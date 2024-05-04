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
            throw new System.NotImplementedException();
        }

        public void Delete(Rate item)
        {
            throw new System.NotImplementedException();
        }

        public Rate Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Rate> GetAll()
        {
            return _context.Rates.ToList();
        }


        public void Update(Rate item)
        {
            throw new System.NotImplementedException();
        }
    }
}
