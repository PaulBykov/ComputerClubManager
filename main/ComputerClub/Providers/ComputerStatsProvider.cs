using ComputerClub.Repositories;
using System;

namespace ComputerClub.Providers
{
    public class ComputerStatsProvider
    {
        private readonly RentsRepository _rentRepository = RepositoryServiceLocator.Resolve<RentsRepository>();
        private readonly ComputersRepository _computerRepository = RepositoryServiceLocator.Resolve<ComputersRepository>();

        public int GetRentedComputersCount()
        {
            try
            {
                return _rentRepository.Count;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int GetFreeComputersCount()
        {
            try
            {
                return _computerRepository.Count - GetRentedComputersCount();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
