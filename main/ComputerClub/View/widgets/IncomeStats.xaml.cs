using System;
using System.Windows.Controls;
using ComputerClub.ViewModel;
using ScottPlot;
using ScottPlot.TickGenerators;
using ScottPlot.TickGenerators.TimeUnits;


namespace ComputerClub.View.widgets
{

    public partial class IncomeStats : UserControl
    {
        public IncomeStats()
        {
            InitializeComponent();
            DataContext = ViewModel = new IncomeStatsVM(IncomeGraph);
            ApplyGraphStyles();
        }

        private IncomeStatsVM ViewModel { get; set; }



        private void ApplyGraphStyles()
        {
            var plot = IncomeGraph.Plot;

            plot.FigureBackground.Color = Color.FromHex("#181818");
            plot.DataBackground.Color = Color.FromHex("#1f1f1f");

            // change axis and grid colors
            plot.Axes.Color(Color.FromHex("#d7d7d7"));
            plot.Grid.MajorLineColor = Color.FromHex("#404040");
            plot.Grid.MinorLineColor = Color.FromHex("#404040");

            plot.Axes.Bottom.FrameLineStyle.Color = Colors.Blue;
            plot.Axes.Bottom.TickLabelStyle.ForeColor = Colors.Magenta;
            plot.Axes.Bottom.MajorTickStyle.Color = Colors.Magenta;
            plot.Axes.Bottom.MinorTickStyle.Color = Colors.Magenta;
            

            // custom labels
            plot.RenderManager.RenderStarting += (s, e) =>
            {
                Tick[] ticks = plot.Axes.Bottom.TickGenerator.Ticks;
                for (int i = 0; i < ticks.Length; i++)
                {
                    DateTime dt = DateTime.FromOADate(ticks[i].Position);
                    string label = $"{dt:d}";
                    ticks[i] = new Tick(ticks[i].Position, label);
                }
            };


            // ticks interval
            var xTicks = plot.Axes.DateTimeTicksBottom();
            xTicks.TickGenerator = new DateTimeFixedInterval(
                new Hour(), 24,
                new Hour(), 24,
                dt => new DateTime(dt.Year, dt.Month, dt.Day));

            IncomeGraph.Refresh();
        }
    }
}
