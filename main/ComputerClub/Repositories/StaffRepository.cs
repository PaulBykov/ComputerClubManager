using ComputerClub.Model;
using System.Collections.Generic;
using System.Linq;

namespace ComputerClub.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private ComputerClubContext _context;
        public UserRepository(ComputerClubContext context)
        {
            _context = context;
        }

        public void Add(User item)
        {
            _context.Users.Add(item);
            _context.SaveChanges();
        }

        public bool Has(string login)
        {
            return _context.Users.Any(x => x.Login == login);
        }

        public void Delete(User item)
        {
            _context.Users.Remove(item);
            _context.SaveChanges();
        }

        public User Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
           return _context.Users.ToList();
        }

        public void Update(User item)
        {
            throw new System.NotImplementedException();
        }
    }
}
