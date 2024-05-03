using ComputerClub.Model.Database;
using ComputerClub.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ComputerClub.Repositories
{
    public partial class ComputersRepository : IRepository<Computer>
    {
        private DbSet<Computer> _context;
        private AuthService _auth;

        public ComputersRepository(DbSet<Computer> context)
        {
            _context = context;
            _auth = AuthService.GetInstance();
        }

        private int CurrentClubId { get => _auth.CurrentClub.Id; }


        public ICollection<Computer> GetAllComputers()
        {
            return _context
                .Where(computer => computer.ClubId == CurrentClubId)
                .ToList();
        }

        public void Create(Computer item)
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

        public Computer Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Computer item)
        {
            throw new NotImplementedException();
        }
    }
}
