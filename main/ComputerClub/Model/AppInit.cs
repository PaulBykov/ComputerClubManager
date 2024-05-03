using ComputerClub.Model.Database;
using ComputerClub.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ComputerClub.Model
{
    public static class AppInit
    {
        public static void Init() 
        {
            ComputerClubContext context = new ComputerClubContext();
            context.Computers.Load();
            context.Rates.Load();

            RegisterRepositories(context);
        }

        private static void RegisterRepositories(ComputerClubContext context) 
        {
            RepositoryServiceLocator.Register(new ComputersRepository(context.Computers));
            RepositoryServiceLocator.Register(new RatesRepository(context.Rates));
            RepositoryServiceLocator.Register(context.Rents);
            RepositoryServiceLocator.Register(new StaffRepository(context.Staff));
            RepositoryServiceLocator.Register(new ClubRepository(context.Clubs));
            RepositoryServiceLocator.Register(new IncomeRepository(context.Incomes));
        }
    }
}
