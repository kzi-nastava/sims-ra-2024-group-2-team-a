using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {

    public enum StatisticUpdateAction {Reservation, Cancellation, Postpone, Recommendation };
    public class AccommodationStatisticsService {
        private readonly IAccommodationStatisticsRepository _statisticsRepository =
            RepositoryInjector.GetInstance<IAccommodationStatisticsRepository>();
        public AccommodationStatisticsService() { }

        public bool Update(AccommodationStatistics accommodationStatistics) {
            return _statisticsRepository.Update(accommodationStatistics);
        }

        public void Save(AccommodationStatistics accStatistics) {
            _statisticsRepository.Save(accStatistics);
        }
        public AccommodationStatistics GetByDateForAccommodation(int accommodationId,int year, int month) {
            return _statisticsRepository.GetAll().Find(x => x.Year == year && x.Month == month && x.AccommodationId == accommodationId);
        }
        public List<AccommodationStatistics> GetByYearForAccommodation(int accommodationId,int year) {
            return _statisticsRepository.GetAll().Where(x => x.Year == year && x.AccommodationId == accommodationId).ToList();
        }
        public List<int> GetYearsWithAvailableStatistics() {
            List<int> years = new List<int>();
            foreach (var statistic in _statisticsRepository.GetAll()) {
                if (!years.Contains(statistic.Year)) {
                    years.Add(statistic.Year);
                }
            }
            return years;
        }
        public void UpdateStatistic(int accommodationId, DateOnly startDate, StatisticUpdateAction action) {
            AccommodationStatistics statistic = this.GetByDateForAccommodation(accommodationId, startDate.Year, startDate.Month);
            if (statistic == null) {
                statistic = new AccommodationStatistics(accommodationId, startDate.Year, startDate.Month);
                statistic = UpdateOnAction(statistic, action);
                this.Save(statistic);
            }
            else {
                statistic = UpdateOnAction(statistic, action);
                this.Update(statistic);
            }
        }
        private AccommodationStatistics UpdateOnAction(AccommodationStatistics statistic, StatisticUpdateAction action) {
            switch (action){
                case StatisticUpdateAction.Reservation: {
                        statistic.ReservationsNum++;
                        break;
                    }
                case StatisticUpdateAction.Cancellation: {
                        statistic.CancelledReservationsNum++;
                        break;
                    }
                case StatisticUpdateAction.Postpone: {
                        statistic.PostponedReservationsNum++;
                        break;
                    }
                case StatisticUpdateAction.Recommendation: {
                        statistic.RenovationsRecommendedNum++;
                        break;
                    }
            }

            return statistic;
        }
    }
}
