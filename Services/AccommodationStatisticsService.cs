using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
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

        public void UpdatePostponedStatistics(int accId, DateOnly oldStartDate, DateOnly oldEndDate, DateOnly newStartDate, DateOnly newEndDate) {
            AccommodationStatistics oldReservationStatistic = this.GetByDateForAccommodation(accId, oldStartDate.Year, oldStartDate.Month);
            oldReservationStatistic.PostponedReservationsNum++;
            this.Update(oldReservationStatistic);

            ReduceReservedDays(accId, oldStartDate, oldEndDate);

            UpdateReservationStatisticsAndCheckDates(accId, newStartDate, newEndDate);
        }

        private void ReduceReservedDays(int accId, DateOnly startDate, DateOnly endDate) {
            if (startDate.Month == endDate.Month) {
                AccommodationStatistics statistic = this.GetByDateForAccommodation(accId, startDate.Year, startDate.Month);
                statistic.DaysReserved -= CalculateDaysDiffrence(startDate,endDate);
                this.Update(statistic);
            }
            else {
                DateOnly newEndDate = new DateOnly(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
                AccommodationStatistics firstStatistic = this.GetByDateForAccommodation(accId, startDate.Year, startDate.Month);
                firstStatistic.DaysReserved -= CalculateDaysDiffrence(startDate, newEndDate);
                this.Update(firstStatistic);

                DateOnly newStartDate = new DateOnly(endDate.Year, endDate.Month, 1);
                AccommodationStatistics secondStatistic = this.GetByDateForAccommodation(accId, newStartDate.Year, newStartDate.Month);
                secondStatistic.DaysReserved -= CalculateDaysDiffrence(newStartDate, endDate);
                this.Update(secondStatistic);
            }
        }

        public void UpdateRecommendationStatistics(int accommodationId, DateOnly startDate) {
            AccommodationStatistics accommodationStatistics = this.GetByDateForAccommodation(accommodationId,startDate.Year,startDate.Month);
            accommodationStatistics.RenovationsRecommendedNum++;
            this.Update(accommodationStatistics);
        }

        public void UpdateCancellationStatisticsAndCheckDates(int accommodationId, DateOnly startDate, DateOnly endDate) {
            if (startDate.Month == endDate.Month) {
                UpdateCancellationStatistics(accommodationId,startDate,endDate, true);
            }
            else {
                DateOnly newEndDate = new DateOnly(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
                UpdateCancellationStatistics(accommodationId,startDate,newEndDate, true);
                
                DateOnly newStartDate = new DateOnly(endDate.Year, endDate.Month, 1);
                UpdateCancellationStatistics(accommodationId,newStartDate,endDate, false);
            }
        }
        public void UpdateCancellationStatistics(int accommodationId, DateOnly startDate, DateOnly endDate, bool addCancellations) {
            AccommodationStatistics statistics = this.GetByDateForAccommodation(accommodationId,startDate.Year,startDate.Month);
            int days = this.CalculateDaysDiffrence(startDate, endDate);
            statistics.DaysReserved -= days;
            
            if(addCancellations)
                statistics.CancelledReservationsNum++;
            this.Update(statistics);

        }
        public void UpdateReservationStatisticsAndCheckDates(int accommodationId, DateOnly startDate, DateOnly endDate) {
            if (startDate.Month == endDate.Month) {
                UpdateReservationStatistics(accommodationId, startDate, endDate, true);
            }
            else {
                DateOnly newEndDate = new DateOnly(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
                UpdateReservationStatistics(accommodationId, startDate, newEndDate, true);

                DateOnly newStartDate = new DateOnly(endDate.Year, endDate.Month, 1);
                UpdateReservationStatistics(accommodationId, newStartDate, endDate, false);
            }
        }
        private void UpdateReservationStatistics(int accommodationId, DateOnly startDate, DateOnly endDate, bool addReservation) {
            AccommodationStatistics statistic = this.GetByDateForAccommodation(accommodationId, startDate.Year, startDate.Month);
            if (statistic == null) {
                statistic = new AccommodationStatistics(accommodationId, startDate.Year, startDate.Month);
                if (addReservation)
                    statistic.ReservationsNum++;
                statistic.DaysReserved += this.CalculateDaysDiffrence(startDate, endDate);
                
                this.Save(statistic);
                return;
            }

            if (addReservation)
                statistic.ReservationsNum++;
            statistic.DaysReserved += this.CalculateDaysDiffrence(startDate, endDate);
            this.Update(statistic);
        }

        private int CalculateDaysDiffrence(DateOnly startDate, DateOnly endDate) {
            DateTime dateTime1 = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            DateTime dateTime2 = new DateTime(endDate.Year, endDate.Month, endDate.Day);

            int differenceInDays = (dateTime1 - dateTime2).Days;
            return differenceInDays;
        }
    }
}
