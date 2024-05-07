using ComputerClub.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace ComputerClub.Model
{
    public static class AppInit
    {
        public static void Init() 
        {
            ComputerClubContext context = new ComputerClubContext();
            context.Computers.Load();
            context.Rates.Load();
            context.Rents.Load();
            context.Staff.Load();
            context.Clubs.Load();

            context.Staff.Include(s => s.Clubs);
            context.Clubs.Include(c => c.Staff);



            RegisterRepositories(context);
        }

        private static void RegisterRepositories(ComputerClubContext context) 
        {
            RepositoryServiceLocator.Register(new ComputersRepository(context));
            RepositoryServiceLocator.Register(new RatesRepository(context));
            RepositoryServiceLocator.Register(new RentsRepository(context));
            RepositoryServiceLocator.Register(new StaffRepository(context));
            RepositoryServiceLocator.Register(new ClubRepository(context));
            RepositoryServiceLocator.Register(new IncomeRepository(context));
        }
    }
}
