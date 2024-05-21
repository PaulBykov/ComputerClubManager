using ComputerClub.Model;
using ComputerClub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


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
            try
            {
                _context.Incomes.Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Delete(Income item)
        {
            try
            {
                _context.Incomes.Remove(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Income Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Income> GetAll()
        {
            try
            {

                var clubId = _auth.CurrentClub?.Id;
                return _context.Incomes.Where(i => i.ClubId.Equals(clubId)).ToList();
            }
            catch (Exception e)
            {
            }

            return new List<Income>();
        }

        public void Update(Income item)
        {
            throw new NotImplementedException();
        }
    }
}
