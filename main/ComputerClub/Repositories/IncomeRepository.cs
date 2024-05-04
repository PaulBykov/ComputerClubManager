using ComputerClub.Model;
using ComputerClub.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ComputerClub.Repositories
{
    public class IncomeRepository : IRepository<Income>
    {
        private ComputerClubContext _context;
        private AuthService _auth;

        public IncomeRepository(ComputerClubContext context)
        {
            _context = context;
            _auth = AuthService.GetInstance();
        }

        private int CurrentClubId { get => _auth.CurrentClub.Id; }

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
            return _context.Incomes.ToList();
        }

        public void Update(Income item)
        {
            throw new NotImplementedException();
        }
    }
}
