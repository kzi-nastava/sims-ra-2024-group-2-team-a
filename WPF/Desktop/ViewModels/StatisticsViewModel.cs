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

        public IEnumerable<StatisticsPeriod> StatisticsPeriodValues {
            get { 
                return Enum.GetValues(typeof(StatisticsPeriod)) as StatisticsPeriod[]; 
            }
        }

        private StatisticsPeriod _period;
        public StatisticsPeriod Period { 
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

            // Retrieve SolidColorBrushes from the application resources
            SolidColorBrushes.Add(Application.Current.Resources["HardGreen"] as SolidColorBrush);
            SolidColorBrushes.Add(Application.Current.Resources["Green"] as SolidColorBrush);
            SolidColorBrushes.Add(Application.Current.Resources["SoftGreen"] as SolidColorBrush);

            LoadStatistics();
            LoadLocationStatistics();
            LoadLanguageStatistics();
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
                case StatisticsPeriod.AllTime:
                    LoadAllTime(SeriesCollection);
                    break;
                case StatisticsPeriod.ThisYear:
                    LoadThisYear(SeriesCollection);
                    break;
                default:
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
                Values = new ChartValues<double> { _tourRequestService.GetThisYearAcceptedPercentage(UserId) },
                Fill = SolidColorBrushes[0]
            });
            collection.Add(new PieSeries {
                Title = "Not accepted",
                Values = new ChartValues<double> { _tourRequestService.GetThisYearNotAcceptedPercentage(UserId) },
                Fill = SolidColorBrushes[2]
            });
            AvgPeopleCount = _tourRequestService.GetThisYearAveragePassengerNumber(UserId);
        }
    }
}
