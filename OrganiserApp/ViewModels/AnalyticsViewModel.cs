using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using OrganiserApp.Models;
using OrganiserApp.Services;
using OrganiserApp.Views.Event;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.ViewModels
{
    public partial class AnalyticsViewModel : BaseViewModel
    {
        public ObservableCollection<ISeries> TicketTypeStatsSeries { get; set; } = new();
        public ObservableCollection<ISeries> TickeTypePercentageSeries { get; set; } = new();
        public ObservableCollection<ISeries> GenderSeries { get; set; } = new();
        public ObservableCollection<ISeries> AgesSeries { get; set; } = new();
        public ObservableCollection<ISeries> CitiesSeries { get; set; } = new();
        public ObservableCollection<Axis> XAxes { get; set; } = new();
        public ObservableCollection<Axis> YAxes { get; set; } = new();
        public ObservableCollection<Axis> XAxesPercentage { get; set; } = new();
        public ObservableCollection<Axis> YAxesPercentage { get; set; } = new();
        public ObservableCollection<Axis> XAxesAges { get; set; } = new();
        public ObservableCollection<Axis> YAxesAges { get; set; } = new();
        public List<int> Sales = new();
        public List<int> Reserved = new();
        public List<int> Canceled = new();
        public List<int> Capacity = new();
        public List<int> Unavailable = new();
        public List<double> PercentageSold = new();
        public List<string> AxisLabels = new();

        [ObservableProperty]
        TicketTypesAnalytics ticketTypeStatistics;

        private readonly AnalyticsService analyticsService;
        private readonly IConnectivity connectivity;
        string EventUuid;
        public AnalyticsViewModel(AnalyticsService analyticsService, IConnectivity connectivity)
        {
            Title = "Ticket sales analytics";

            this.analyticsService = analyticsService;
            this.connectivity = connectivity;
        }

        public async void Init()
        {
            Title = "Analytics";
            EventUuid = Preferences.Get("EventUuid", null);

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");

            ClearData();
            await GetTicketTypeSalesAnalyticsAsync();
            await GetGenderAnalyticsAsync();
            await GetAgesAnalyticsAsync();
        }

        [ICommand]
        async Task GetTicketTypeSalesAnalyticsAsync()
        {
            if (IsBusy || EventUuid is null)
                return;

            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                IsBusy = true;

                ClearData();
                var TicketTypeStatistics = await analyticsService.GetTicketTypeSalesAnalytics(EventUuid);

                foreach (var item in TicketTypeStatistics.Content)
                {
                    var unavailable = item.Amount + item.Reserved;
                    double percentage = ((double)unavailable / item.Capacity) * 100;
                    Canceled.Add(item.Canceled);
                    Sales.Add(item.Amount);
                    Reserved.Add(item.Reserved);
                    Capacity.Add(item.Capacity);
                    Unavailable.Add(unavailable);
                    PercentageSold.Add(percentage);

                    item.Name = item.Name.Length > 10 ? $"{item.Name.Substring(0, 10)}..." : item.Name;
                    AxisLabels.Add(item.Name);
                }

                InitTicketSalesGraph();
                InitTicketPercentageGraph();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get statistics: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        async Task GetGenderAnalyticsAsync()
        {
            if (IsBusy || EventUuid is null)
                return;

            try
            {
                IsBusy = true;

                var GenderAnalytics = await analyticsService.GetGenderAnalytics(EventUuid);

                foreach (var item in GenderAnalytics.Content)
                {
                    GenderSeries.Add(new PieSeries<int>
                    {
                        Values = new List<int> { item.Amount }, InnerRadius = 100,
                        Name = item.Gender == "M" ? "Male" : "Female"
                    });;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get statistics: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        async Task GetAgesAnalyticsAsync()
        {
            if (IsBusy || EventUuid is null)
                return;

            try
            {
                IsBusy = true;

                var AgesAnalytics = await analyticsService.GetAgesAnalytics(EventUuid);
                var XAxesList = new List<string>();

                foreach (var item in AgesAnalytics.Content)
                {
                    if (item.Amount > 0)
                    {
                        AgesSeries.Add(new ColumnSeries<int>
                        {
                            Values = new List<int> { item.Amount },
                            Name = item.Age
                        });
                    }
                }

                XAxesList.Add("Ages");
                XAxesAges.Add(new Axis
                {
                    Labels = XAxesList,
                    TextSize = 25,
                });

                YAxesAges.Add(new Axis
                {
                    TextSize = 25,
                    MinLimit = 0
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get statistics: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        async Task GetCitiesAnalyticsAsync()
        {
            if (IsBusy || EventUuid is null)
                return;

            try
            {
                IsBusy = true;

                var CitiesAnalytics = await analyticsService.GetCitiesAnalytics(EventUuid);

                foreach (var item in CitiesAnalytics.Content)
                {
                    
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get statistics: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        void ClearData()
        {
            Canceled.Clear();
            Sales.Clear();
            Reserved.Clear();
            Capacity.Clear();
            Unavailable.Clear();
            AxisLabels.Clear();

            TicketTypeStatsSeries.Clear();
            GenderSeries.Clear();
            AgesSeries.Clear();
            CitiesSeries.Clear();
        }

        void InitTicketSalesGraph()
        {
            TicketTypeStatsSeries.Add(new StackedColumnSeries<int>
            {
                Name = "Sold",
                Values = Sales,
                Stroke = null,
                Fill = new SolidColorPaint(new SKColor(61, 220, 151)),
                DataLabelsPaint = new SolidColorPaint(new SKColor(0, 45, 45)),
                DataLabelsSize = 30,
                DataLabelsPosition = DataLabelsPosition.Middle
            });

            TicketTypeStatsSeries.Add(new StackedColumnSeries<int>
            {
                Name = "Reserved",
                Values = Reserved,
                Stroke = null,
                Fill = new SolidColorPaint(new SKColor(255, 164, 0)),
                DataLabelsPaint = new SolidColorPaint(new SKColor(45, 45, 45)),
                DataLabelsSize = 30,
                DataLabelsPosition = DataLabelsPosition.Middle,
            });

            TicketTypeStatsSeries.Add(new StackedColumnSeries<int>
            {
                Name = "Canceled",
                Values = Canceled,
                Stroke = null,
                Fill = new SolidColorPaint(new SKColor(255, 73, 92)),
                DataLabelsPaint = new SolidColorPaint(new SKColor(45, 45, 45)),
                DataLabelsSize = 30,
                DataLabelsPosition = DataLabelsPosition.Middle
            });

            XAxes.Add(new Axis
            {
                Labels = AxisLabels,
                TextSize = 25,
            });

            YAxes.Add(new Axis
            {
                TextSize = 25,
            });
        }

        void InitTicketPercentageGraph()
        {
            List<double> MaxValues = new();
            for (int i = 0; i < Capacity.Count; i++)
            {
                MaxValues.Add(100);
            }

            TickeTypePercentageSeries.Add(new ColumnSeries<double>
            {
                IsHoverable = false,
                Values = MaxValues,
                Stroke = null,
                Fill = new SolidColorPaint(new SKColor(234, 235, 238)),
                IgnoresBarPosition = true
            });

            TickeTypePercentageSeries.Add(new ColumnSeries<double>
            {
                IsHoverable = false,
                Values = PercentageSold,
                Stroke = null,
                Fill = new SolidColorPaint(new SKColor(61, 220, 151)),
                IgnoresBarPosition = true
            });

            XAxesPercentage.Add(new Axis
            {
                Labels = AxisLabels,
                TextSize = 25,
            });

            YAxesPercentage.Add(new Axis
            {
                TextSize = 25,
                MinLimit = 0,
                MaxLimit = 100
            });
        }
    }
}
