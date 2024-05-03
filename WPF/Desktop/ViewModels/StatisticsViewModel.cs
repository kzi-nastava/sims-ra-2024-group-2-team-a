using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.Desktop.ViewModels
{
    public enum StatisticsPeriod { AllTime, ThisYear }
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        private readonly TourRequestService _tourRequestService = new TourRequestService(RepositoryInjector.GetInstance<ITourRequestRepository>());

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

        public StatisticsViewModel() { }
        public StatisticsViewModel(int userId) { 
            UserId = userId;
            LoadStatistics();
        }

        private void LoadStatistics() {
            SeriesCollection = new SeriesCollection();

            switch (Period) {
                case StatisticsPeriod.AllTime:
                    LoadAllTime(SeriesCollection);
                    break;
                case StatisticsPeriod.ThisYear:
                    LoadThisYear();
                    break;
                default:
                    break;
            }
        }

        private void LoadAllTime(SeriesCollection collection) {
            collection.Clear();
            collection.Add(new PieSeries {
                Title = "Accepted",
                Values = new ChartValues<double> { _tourRequestService.GetAllTimeAcceptedPercentage(UserId) }
            });
            collection.Add(new PieSeries {
                Title = "Not accepted",
                Values = new ChartValues<double> { _tourRequestService.GetAllTimeNotAcceptedPercentage(UserId) }
            });
            AvgPeopleCount = _tourRequestService.GetAllTimeAveragePassengerNumber(UserId);
        }

        private void LoadThisYear() {

        }
    }
}
