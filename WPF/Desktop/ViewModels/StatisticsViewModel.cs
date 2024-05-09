using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BookingApp.WPF.Desktop.ViewModels
{
    public enum StatisticsPeriod { AllTime, ThisYear }
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        private readonly TourRequestService _tourRequestService = ServicesPool.GetService<TourRequestService>();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int UserId { get; set; }

        public List<string> Periods { get; set; } = new List<string>(); 

        private string _period;
        public string Period { 
            get {
                return _period;
            }
            set {
                if (_period != value) {
                    _period = value;
                    OnPropertyChanged();
                    LoadStatistics();
                }                
            }
        }

        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection {
            get { return _seriesCollection; }
            set {
                _seriesCollection = value;
                OnPropertyChanged(nameof(SeriesCollection));
            }
        }

        private SeriesCollection _locationCollection = new SeriesCollection();
        public SeriesCollection LocationCollection {
            get { return _locationCollection; }
            set {
                _locationCollection = value;
                OnPropertyChanged(nameof(LocationCollection));
            }
        }

        private SeriesCollection _languageCollection = new SeriesCollection();
        public SeriesCollection LanguageCollection {
            get { return _languageCollection; }
            set {
                _languageCollection = value;
                OnPropertyChanged(nameof(LanguageCollection));
            }
        }

        private double _avgPeopleCount;
        public double AvgPeopleCount {
            get {
                return _avgPeopleCount;
            }
            set {
                if (value != _avgPeopleCount) {
                    _avgPeopleCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<SolidColorBrush> SolidColorBrushes { get; set; }

        public StatisticsViewModel() { }
        public StatisticsViewModel(int userId) { 
            UserId = userId;

            SolidColorBrushes = new List<SolidColorBrush>();
            SolidColorBrushes.Add(Application.Current.Resources["HardGreen"] as SolidColorBrush);
            SolidColorBrushes.Add(Application.Current.Resources["Green"] as SolidColorBrush);
            SolidColorBrushes.Add(Application.Current.Resources["SoftGreen"] as SolidColorBrush);

            LoadPeriods();
            Period = Periods[0];
            LoadLocationStatistics();
            LoadLanguageStatistics();
        }

        private void LoadPeriods() {
            Periods.Add("All-time");
            foreach (var period in _tourRequestService.GetRequestYears(UserId)) {
                Periods.Add(period.ToString());
            }
        }

        private void LoadLocationStatistics() {
            int brushIndex = 0;

            foreach (var pair in _tourRequestService.GetRequestsByLocations(UserId)) {
                //LocationLabels.Add(pair.Key.City + " - " + pair.Key.Country);
                LocationCollection.Add(new RowSeries {
                    Title = pair.Key.City + " - " + pair.Key.Country,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(pair.Value) },
                    Fill = SolidColorBrushes[brushIndex %  SolidColorBrushes.Count]
                });  

                brushIndex++;
            }
        }

        private void LoadLanguageStatistics() {
            int brushIndex = 0;

            foreach (var pair in _tourRequestService.GetRequestsByLanguages(UserId)) {
                LanguageCollection.Add(new RowSeries {
                    Title = pair.Key.Name,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(pair.Value) },
                    Fill = SolidColorBrushes[brushIndex % SolidColorBrushes.Count]
                });

                brushIndex++;
            }
        }

        private void LoadStatistics() {
            SeriesCollection = new SeriesCollection();

            switch (Period) {
                case "All-time":
                    LoadAllTime(SeriesCollection);
                    break;
                default:
                    LoadThisYear(SeriesCollection);
                    break;
            }
        }

        private void LoadAllTime(SeriesCollection collection) {
            collection.Clear();
            collection.Add(new PieSeries {
                Title = "Accepted",
                Values = new ChartValues<double> { _tourRequestService.GetAllTimeAcceptedPercentage(UserId) },
                Fill = SolidColorBrushes[0]
            });
            collection.Add(new PieSeries {
                Title = "Not accepted",
                Values = new ChartValues<double> { _tourRequestService.GetAllTimeNotAcceptedPercentage(UserId) },
                Fill = SolidColorBrushes[2]
            });
            AvgPeopleCount = _tourRequestService.GetAllTimeAveragePassengerNumber(UserId);
        }

        private void LoadThisYear(SeriesCollection collection) {
            collection.Clear();
            collection.Add(new PieSeries {
                Title = "Accepted",
                Values = new ChartValues<double> { _tourRequestService.GetAcceptedPercentageForYear(UserId, int.Parse(Period)) },
                Fill = SolidColorBrushes[0]
            });
            collection.Add(new PieSeries {
                Title = "Not accepted",
                Values = new ChartValues<double> { _tourRequestService.GetNotAcceptedPercentageForYear(UserId, int.Parse(Period)) },
                Fill = SolidColorBrushes[2]
            });
            AvgPeopleCount = _tourRequestService.GetAveragePassengerNumberForYear(UserId, int.Parse(Period));
        }
    }
}
