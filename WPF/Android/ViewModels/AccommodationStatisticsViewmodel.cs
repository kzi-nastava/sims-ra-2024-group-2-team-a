using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using BookingApp.WPF.Utils.Commands;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BookingApp.WPF.Android.ViewModels {
    public class AccommodationStatisticsViewmodel : INotifyPropertyChanged {

        private readonly AccommodationStatisticsService _statisticsService = ServicesPool.GetService<AccommodationStatisticsService>();
        public AccommodationDTO AccommodationDTO { get; set; }
        public SeriesCollection ChartPieYearlyList { get; set; }

        private SeriesCollection _chartpieMonthly;
        public SeriesCollection ChartPieMonthlyList {
            get { return _chartpieMonthly; }
            set {
                if (value != _chartpieMonthly) {
                    _chartpieMonthly = value;
                    OnPropertyChanged();
                }
            }
        }
        public ChartValues<string> Years { get; set; }
        public ChartValues<string> Months { get; set; }
        public ChartValues<int> ReservationsYearly { get; set; }
        public ChartValues<int> CancellationsYearly { get; set; }
        public ChartValues<int> PostponedYearly { get; set; }
        public ChartValues<int> RecommendationsYearly { get; set; }
        public ChartValues<int> ReservationsMonthly { get; set; }
        public ChartValues<int> CancellationsMonthly { get; set; }
        public ChartValues<int> PostponedMonthly { get; set; }
        public ChartValues<int> RecommendationsMonthly { get; set; }
        public AccommodationStatistics BusiestYear { get; set; }

        public string _busiestMonth;
        public string BusiestMonth {
            get { return _busiestMonth; }
            set {
                if (value != _busiestMonth) {
                    _busiestMonth = value;
                    OnPropertyChanged();
                }
            }
        }

        public int _reservationsInSelectedYear;
        public int ReservationsInSelectedYear {
            get { return _reservationsInSelectedYear; }
            set {
                _reservationsInSelectedYear = value;
                OnPropertyChanged();
            }
        }

        private string _selectedYear;
        public string SelectedYear {
            get { return _selectedYear; }
            set {
                if (value != _selectedYear) {
                    _selectedYear = value;
                    OnPropertyChanged();
                }
            }
        }

        public int _totalReservations;
        public int TotalReservations {
            get {
                return _totalReservations;
            }
            set {
                if (value != _totalReservations) {
                    _totalReservations = value;
                    OnPropertyChanged();
                }
            }
        }
        public Func<double, string> YAxisFormatter { get; set; }

        public Separator YAxisSeparator { get; set; }
        public AccommodationStatisticsViewmodel(AccommodationDTO accomodationDTO) {
            this.AccommodationDTO = accomodationDTO;

            ReservationsYearly = new ChartValues<int>();
            CancellationsYearly = new ChartValues<int>();
            PostponedYearly = new ChartValues<int>();
            RecommendationsYearly = new ChartValues<int>();

            ReservationsMonthly = new ChartValues<int>();
            CancellationsMonthly = new ChartValues<int>(); 
            PostponedMonthly = new ChartValues<int>();
            RecommendationsMonthly = new ChartValues<int>();

            Years = new ChartValues<string>();
            TotalReservations = 0;
            Months = new ChartValues<string> { "Jan","Feb","Mar","Apr","May","June","July","Aug","Sep","Oct","Nov","Dec"};
            YAxisFormatter = value => value.ToString("N0");
            YAxisSeparator = new Separator { Step = 1 };
            Update();
        }
        public void SelectionChangedExecute() {
            this.UpdateMonthlyStatistics();
        }

        public void Update() {
            UpdateYearlyStatistics();
            UpdateMonthlyStatistics();
        }

        public void UpdateMonthlyStatistics() {
            ReservationsMonthly.Clear();
            CancellationsMonthly.Clear();
            PostponedMonthly.Clear();
            RecommendationsMonthly.Clear();

            ReservationsInSelectedYear = 0;
            int totalDays = 0;
            
            for (int i=1;i<13;i++) {
                AccommodationStatistics accommodationStatistics = _statisticsService.GetByDateForAccommodation(AccommodationDTO.Id
                    , int.Parse(SelectedYear), i);
                if (accommodationStatistics == null) {
                    ReservationsMonthly.Add(0);
                    CancellationsMonthly.Add(0);
                    PostponedMonthly.Add(0);
                    RecommendationsMonthly.Add(0);
                }
                else {
                    ReservationsMonthly.Add(accommodationStatistics.ReservationsNum);
                    CancellationsMonthly.Add(accommodationStatistics.CancelledReservationsNum);
                    PostponedMonthly.Add(accommodationStatistics.PostponedReservationsNum);
                    RecommendationsMonthly.Add(accommodationStatistics.RenovationsRecommendedNum);
                    totalDays += accommodationStatistics.DaysReserved;
                    ReservationsInSelectedYear += accommodationStatistics.ReservationsNum;
                }
            }

            ChartPieMonthlyList = new SeriesCollection() {
                new PieSeries{
                    Title="Days reserved",
                    Values = new ChartValues<ObservableValue>{ new ObservableValue(totalDays) },
                    DataLabels = true,
                    Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#12372A"))
                },
                new PieSeries{
                    Title="Days not reserved",
                    Values = new ChartValues<ObservableValue>{ new ObservableValue(365 - totalDays) },
                    DataLabels = true,
                    Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ADBC9F"))
                }
            };

            BusiestMonth = Months[_statisticsService.GetBusiestMonthStatistics(AccommodationDTO.Id, int.Parse(SelectedYear)).Month - 1];
        }
        public void UpdateYearlyStatistics() {
            int totalDays = 0;

            foreach (var v in _statisticsService.GetYearsWithAvailableStatistics(AccommodationDTO.Id)) {
                Years.Add(v.ToString());
            }

            SelectedYear = Years[0];

            foreach (var statistic in _statisticsService.GetYearlyStatistics(AccommodationDTO.Id)) {
                ReservationsYearly.Add(statistic.ReservationsNum);
                CancellationsYearly.Add(statistic.CancelledReservationsNum);
                PostponedYearly.Add(statistic.PostponedReservationsNum);
                RecommendationsYearly.Add(statistic.RenovationsRecommendedNum);
                TotalReservations += statistic.ReservationsNum;
                totalDays += statistic.DaysReserved;
            }

            ChartPieYearlyList = new SeriesCollection() {
                new PieSeries{
                    Title="Days reserved",
                    Values = new ChartValues<ObservableValue>{ new ObservableValue(totalDays) },
                    DataLabels = true,
                    Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#12372A"))
                },
                new PieSeries{
                    Title="Days not reserved",
                    Values = new ChartValues<ObservableValue>{ new ObservableValue(_statisticsService.GetDaysFromFirstReservation(AccommodationDTO.Id) - totalDays) },
                    DataLabels = true,
                    Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ADBC9F"))
                }
            };

            BusiestYear = _statisticsService.GetBusiestYearStatistics(AccommodationDTO.Id);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
