using CommunityToolkit.Mvvm.ComponentModel;
using ComputerClub.Model.Database;
using ComputerClub.Repositories;
using System.Collections.ObjectModel;

namespace ComputerClub.ViewModel
{
    public partial class RatesPageVM : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Rate> _rates;

        public RatesPageVM() 
        {
            Rates = new ObservableCollection<Rate>(
                RepositoryServiceLocator.Resolve<RatesRepository>().GetAllRates()
            );
        }
    }
}
