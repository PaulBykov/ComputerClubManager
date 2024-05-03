using ComputerClub.Model.Database;
using ComputerClub.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ComputerClub.Repositories
{
    public class IncomeRepository : IRepository<Income>
    {
        private DbSet<Income> _context;
        private AuthService _auth;

        public IncomeRepository(DbSet<Income> context)
        {
            _context = context;
            _auth = AuthService.GetInstance();
        }

        private int CurrentClubId { get => _auth.CurrentClub.Id; }




        public void Create(Income item)
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

        public Income Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Income item)
        {
            throw new NotImplementedException();
        }
    }
}
