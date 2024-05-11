using ComputerClub.Model;
using ComputerClub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace ComputerClub.Repositories
{
    public partial class ComputersRepository : IRepository<Computer>, IChangesNotifier<Computer>
    {
        private ComputerClubContext _context;
        private AuthService _auth = AuthService.GetInstance();

        public event EventHandler DatabaseChanges;

        public ComputersRepository(ComputerClubContext context)
        {
            _context = context;
        }

        private int CurrentClubId => _auth.CurrentClub.Id;

        public int Count => GetAll().Count();
        public IEnumerable<Computer> GetAll()
        {
            return _context.Computers
                .Where(computer => computer.ClubId == CurrentClubId)
                .ToList();
        }

        public void AddMany(int count, Rate rate)
        {
            try
            {
                for (int i = 0; i < count; i++)
                {
                    var computer = new Computer() { ClubId = _auth.CurrentClub.Id, RateName = rate.Name };
                    _context.Computers.Add(computer);
                }

                _context.SaveChanges();
                DatabaseChanges?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex) 
            {
                MessageBox.Show("An error accureted during adding computers" + ex.Message);
            }
        }

        public Computer Get(int id)
        {
            return _context.Computers.Where(c => c.Id == id).FirstOrDefault();
        }

        public void Add(Computer item)
        {
            try
            {
                _context.Computers.Add(item);

                _context.SaveChanges();
                DatabaseChanges?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error accureted during adding computers" + ex.Message);
            }
        }

        public void Update(Computer item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Computer item)
        {
            try
            {
                _context.Computers.Remove(item);
                _context.SaveChanges();
                DatabaseChanges?.Invoke(this, EventArgs.Empty);
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error during removing the computer!" + ex.Message);
            }
        }
    }
}
