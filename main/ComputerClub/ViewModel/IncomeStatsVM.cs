using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComputerClub.Properties;
using ComputerClub.Providers;
using ComputerClub.Services;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SkiaSharp;
using SkiaSharp.Views.WPF;
using TimeSpan = System.TimeSpan;


namespace ComputerClub.ViewModel
{
    public partial class IncomeStatsVM : ObservableObject
    {
        private readonly IncomeStatsProvider _provider = new();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CurrentClubBalance))]
        [NotifyPropertyChangedFor(nameof(LastIncome))]
        private int _reportPeriod = 7;


        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DisplayedClubBalance))]
        private decimal _currentClubBalance = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DisplayedLastIncome))]
        private decimal _lastIncome = 0;

        [ObservableProperty] 
        private ObservableCollection<Tuple<DateTime, decimal>> _data;

        // graph line
        [ObservableProperty] 
        private ISeries[] _serie;


        public IncomeStatsVM()
        {
            Refresh();

            AuthService.GetInstance().PropertyChanged += OnPropertyChanged;
        }


        public string DisplayedClubBalance => CurrentClubBalance.ToString("0.00") + Settings.Default.Currency;
        public string DisplayedLastIncome => LastIncome.ToString("0.00") + Settings.Default.Currency;



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
                
                Labeler = (d) => d.ToString() + Settings.Default.Currency,
                LabelsPaint = new SolidColorPaint(SKColors.White),
                SeparatorsPaint = new SolidColorPaint
                {
                    Color = SKColors.White,
                    StrokeThickness = 1,
                    PathEffect = new DashEffect(new float[] { 3, 3 })
                },
            }
        };


        [RelayCommand]
        public void SubmitInfoUpdate()
        {
            Refresh();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            _provider.Period = ReportPeriod;
            UpdateBalanceData();
            UpdateData();
            Serie = new ISeries[]
            {
                GetGraphLine()
            };
        }
        private void UpdateBalanceData()
        {
            try
            {
                if (_provider?.Balance == null)
                {
                    return;

                }

                CurrentClubBalance = (decimal)_provider.Balance;
                LastIncome = _provider.GetTotalIncomeOnPeriod();
            }
            catch (Exception e)
            {

            }
        }
        private void UpdateData()
        {
            Data = new ObservableCollectionListSource<Tuple<DateTime, decimal>>(
                _provider.GraphData);
        }


        private ISeries GetGraphLine()
        {
            ObservableCollection< Tuple<DateTime, decimal> > data = Data;
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
