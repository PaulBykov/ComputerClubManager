using CommunityToolkit.Mvvm.ComponentModel;
using ComputerClub.Providers;
using ScottPlot;
using ScottPlot.WPF;
using WinRT.Interop;

namespace ComputerClub.ViewModel
{
    public partial class IncomeStatsVM : ObservableObject
    {
        private readonly IncomeStatsProvider _provider = new();
        private readonly WpfPlot _graph;

        [ObservableProperty] 
        private decimal _currentClubBalance;

        [ObservableProperty] 
        private decimal _lastWeekIncome;


        public IncomeStatsVM(WpfPlot graphPlot)
        {
            _graph = graphPlot;
            UpdateData();
            BuildGraph();
        }

        private void UpdateData()
        {
            CurrentClubBalance = _provider.Balance;
            LastWeekIncome = _provider.WeekIncome;
        }

        private void BuildGraph()
        {
            var dataX = _provider.GraphOX;
            var dataY = _provider.GraphOY;

            var scatter = _graph.Plot.Add.ScatterLine(dataX, dataY);
            scatter.LineWidth = 5;
            scatter.LineColor = Color.FromHex("#a73d67");

            _graph.Refresh();
        }

    }
}
