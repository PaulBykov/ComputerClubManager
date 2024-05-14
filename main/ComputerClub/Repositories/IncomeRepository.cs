using ComputerClub.Model;
using ComputerClub.Services;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ComputerClub.Repositories
{
    public class IncomeRepository : IRepository<Income>
    {
        private readonly ComputerClubContext _context;
        private readonly AuthService _auth;

        public IncomeRepository(ComputerClubContext context)
        {
            _context = context;
            _auth = AuthService.GetInstance();
        }



        public void Add(Income item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Income item)
        {
            throw new NotImplementedException();
        }

        public Income Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Income> GetAll()
        {
            var clubId = _auth.CurrentClub?.Id;
            return _context.Incomes.Where(i => i.ClubId.Equals(clubId)).ToList();
        }

        public void Update(Income item)
        {
            throw new NotImplementedException();
        }
    }
}
