using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerClub.Repositories;
using ComputerClub.Services;

namespace ComputerClub.Providers
{
    public class IncomeStatsProvider
    {
        private IncomeRepository _incomeRepository = RepositoryServiceLocator.Resolve<IncomeRepository>();
        private AuthService _auth = AuthService.GetInstance();

        
        public IncomeStatsProvider()
        {
            
        }


    }
}
