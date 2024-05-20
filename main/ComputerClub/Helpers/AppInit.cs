using System.Linq;
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.Services;
using Microsoft.EntityFrameworkCore;

namespace ComputerClub.Helpers
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
            context.Sessions.Load();

            context.Users.Include(u => u.Clubs).ToList();
            context.Clubs.Include(c => c.Users).ToList();
            context.Clubs.Include(c => c.Session).ToList();


            AuthService.GetInstance(context);
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
