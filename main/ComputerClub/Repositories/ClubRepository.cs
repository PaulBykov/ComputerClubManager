using ComputerClub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using ComputerClub.View.modal;

namespace ComputerClub.Repositories
{
    public class ClubRepository : IRepository<Club>
    {
        private ComputerClubContext _context;
        public ClubRepository(ComputerClubContext context)
        {
            _context = context;
        }

        public IEnumerable<Club> GetAll()
        {
            return _context.Clubs?.ToList();
        }

        public bool Has(string name)
        {
            try
            {

                return _context.Clubs.Any(c => c.Name == name);

            }
            catch (Exception e)
            {
            }

            return false;
        }

        public void Add(Club item)
        {
            try
            {
                _context.Clubs.Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                NotifyModalWindow.Show(NotifyModalWindow.NotifyKind.Error, "ComputersRepo add:" + ex);
            }
        }

        public void Delete(Club item)
        {
            try
            {
                _context.Clubs.Remove(item);

                foreach (var income in _context.Incomes)
                {
                    if (income.ClubId == item.Id)
                    {
                        _context.Incomes.Remove(income);
                    }
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                NotifyModalWindow.Show(NotifyModalWindow.NotifyKind.Error, "ComputersRepo add:" + ex);
            }

        }

        public Club Get(int id)
        {
            try
            {

                return _context.Clubs.Where(c => c.Id ==  id).FirstOrDefault();
            }
            catch (Exception e)
            {
            }

            return null;
        }

        public void Update(Club item)
        {
            throw new NotImplementedException();
        }
    }
}
