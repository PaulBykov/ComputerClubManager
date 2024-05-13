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
        

        public IncomeStatsProvider()
        {
        }

        private Club Club => AuthService.GetInstance().CurrentClub;

        public int Period { get; set; }
        public decimal? Balance => Club?.Balance;
        public List<Tuple<DateTime, decimal>> GraphData
        {
            get
            {
                List< Tuple<DateTime, decimal> > graphData = new List<Tuple<DateTime, decimal>>();
                DateOnly periodStart = DateOnly.FromDateTime(DateTime.Today).AddDays(-Period);

                var groupedData = _incomeRepository.GetAll()
                    .Where(income => income.Date >= periodStart)
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


        public decimal GetTotalIncomeOnPeriod()
        {
            return SumIncomes(GetIncomesOnPeriod(Period));
        }


        private IEnumerable<Income> GetIncomesOnPeriod(int period)
        {
            List<Income> incomes = new(_incomeRepository.GetAll());

            DateOnly targetDay = DateOnly.FromDateTime(DateTime.Today);
            targetDay = targetDay.AddDays(-period);

            return incomes.Where(i => i.Date >= targetDay);
        }

        private decimal SumIncomes(IEnumerable<Income> incomes)
        {
            return incomes.Sum(i => i.Amount);

        }
    }
}
