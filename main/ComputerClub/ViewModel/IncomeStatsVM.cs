using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using ComputerClub.Providers;

namespace ComputerClub.ViewModel
{
    public partial class IncomeStatsVM : ObservableObject
    {
        private IncomeStatsProvider _provider = new IncomeStatsProvider();

        [ObservableProperty] 
        private SqlMoney _currentClubBalance;

        public IncomeStatsVM() { }

        
    }
}
