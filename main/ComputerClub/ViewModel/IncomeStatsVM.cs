using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using ComputerClub.Providers;

using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;
using SkiaSharp.Views.WPF;
using TimeSpan = System.TimeSpan;


namespace ComputerClub.ViewModel
{
    public partial class IncomeStatsVM : ObservableObject
    {
        private readonly IncomeStatsProvider _provider = new();

        [ObservableProperty] 
        private decimal _currentClubBalance;

        [ObservableProperty] 
        private decimal _lastWeekIncome;

        // graph line
        [ObservableProperty] 
        private ISeries[] _serie;


        public IncomeStatsVM()
        {
            UpdateBalanceData();
            Serie = new ISeries[]
            {
                GetGraphLine()
            };
        }

        public Axis[] XAxes { get; set; } =
        {
            new DateTimeAxis(TimeSpan.FromDays(1), date => date.ToString("d"))
            {
                TextSize = 18,
                LabelsPaint = new SolidColorPaint(SKColors.White),
                SeparatorsPaint = new SolidColorPaint
                {
                    Color = SKColors.White,
                    StrokeThickness = 1,
                    PathEffect = new DashEffect(new float[] { 3, 3 })
                },
            }
        };

        public Axis[] YAxes { get; set; } =
        {
            new Axis()
            {
                TextSize = 18,
                LabelsPaint = new SolidColorPaint(SKColors.White),
                SeparatorsPaint = new SolidColorPaint
                {
                    Color = SKColors.White,
                    StrokeThickness = 1,
                    PathEffect = new DashEffect(new float[] { 3, 3 })
                },
            }
        };


        private void UpdateBalanceData()
        {
            CurrentClubBalance = _provider.Balance;
            LastWeekIncome = _provider.WeekIncome;
        }

        private ISeries GetGraphLine()
        {
            List< Tuple<DateTime, decimal> > data = _provider.GraphData;
            ObservableCollection<DateTimePoint> points = new();


            foreach(var income in data)
            {
                points.Add(new DateTimePoint(income.Item1, decimal.ToDouble(income.Item2)));
            }


            return new LineSeries<DateTimePoint>()
            {
                Values = points,

                GeometryFill = new SolidColorPaint(Color.FromRgb(251, 70, 143).ToSKColor()),
                GeometryStroke = new SolidColorPaint(Color.FromRgb(255, 255, 255).ToSKColor()),
                Stroke = new SolidColorPaint(Color.FromRgb(251, 70, 143).ToSKColor()),
                Fill = new SolidColorPaint(Color.FromArgb(195, 167, 62, 104).ToSKColor()),
            };
        }
    }
}
