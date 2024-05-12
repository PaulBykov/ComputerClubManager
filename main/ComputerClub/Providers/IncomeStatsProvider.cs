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


        public DateTime[] GraphOX
        {
            get
            {
                DateTime[] dates = new DateTime[Distance];
                DateTime today = DateTime.Today;

                for (int i = 0; i < Distance; i++)
                {
                    dates[i] = today.AddDays(-i);
                }

                return dates;
            }
        }

        public decimal[] GraphOY
        {
            get
            {
                decimal[] graphData = new decimal[Distance];
                Income[] incomes = _incomeRepository.GetAll().ToArray();
                
                for (int i = 0; i < Distance; i++)
                {
                    if (i < incomes.Length)
                    {
                        graphData[i] = incomes[i].Amount;
                    }
                    else
                    {
                        graphData[i] = 0.0m;
                    }
                }

                return graphData;
            }
        }
    }
}
