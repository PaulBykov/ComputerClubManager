using System;
using System.Collections.Generic;
using System.Linq;
using ComputerClub.Model;
using ComputerClub.Repositories;
using ComputerClub.Services;

namespace ComputerClub.Providers
{
    public class IncomeStatsProvider
    {
        private readonly IncomeRepository _incomeRepository = RepositoryServiceLocator.Resolve<IncomeRepository>();
        private readonly Club _club = AuthService.GetInstance().CurrentClub;

        public IncomeStatsProvider(int distance = 7)
        {
            Distance = distance;
        }

        public int Distance { get; set; }
        public decimal Balance => _club.Balance;
        public decimal WeekIncome
        {
            get 
            {
                List<Income> incomes = new List<Income>(_incomeRepository.GetAll());

                DateOnly targetDay = DateOnly.FromDateTime(DateTime.Today);
                targetDay = targetDay.AddDays(-Distance);


                decimal filteredAmount = incomes
                    .Where(i => i.Date >= targetDay)
                    .Sum(i => i.Amount);

                return filteredAmount;
            }
        }


        public List<Tuple<DateTime, decimal>> GraphData
        {
            get
            {
                List< Tuple<DateTime, decimal> > graphData = new List<Tuple<DateTime, decimal>>();

                var groupedData = _incomeRepository.GetAll()
                    .GroupBy(income => income.Date)
                    .Select(group => 
                        new
                        {
                            Date = group.Key.ToDateTime(TimeOnly.MinValue),
                            Amount = group.Sum(item => item.Amount)
                        }
                    )
                    .OrderBy(item => item.Date)
                    .ToArray();

  
                foreach (var dayIncome in groupedData)
                {
                    graphData.Add(Tuple.Create(dayIncome.Date, dayIncome.Amount));
                }

                return graphData;
            }
        }
    }
}
