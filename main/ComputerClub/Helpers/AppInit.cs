using ComputerClub.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            context.Users.Load();
            context.Clubs.Load();

            context.Users.Include(s => s.Clubs).ToList();
            context.Clubs.Include(c => c.Users).ToList();


            RegisterRepositories(context);
        }

        private static void RegisterRepositories(ComputerClubContext context) 
        {
            RepositoryServiceLocator.Register(new ComputersRepository(context));
            RepositoryServiceLocator.Register(new RatesRepository(context));
            RepositoryServiceLocator.Register(new RentsRepository(context));
            RepositoryServiceLocator.Register(new UserRepository(context));
            RepositoryServiceLocator.Register(new ClubRepository(context));
            RepositoryServiceLocator.Register(new IncomeRepository(context));
            RepositoryServiceLocator.Register(new SessionRepository(context));
        }
    }
}
