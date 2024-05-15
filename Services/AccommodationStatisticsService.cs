using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class AccommodationStatisticsService {

        private readonly IAccommodationStatisticsRepository _statisticsRepository;

        private AccommodationReservationService _reservationService;

        private AccommodationService _accommodationService;

        public AccommodationStatisticsService(IAccommodationStatisticsRepository statisticsRepository) { 
            _statisticsRepository = statisticsRepository;
        }

        public void InjectServices(AccommodationReservationService reservationService, AccommodationService accService) {
            _reservationService = reservationService;
            _accommodationService = accService;
        }

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

        
        public List<AccommodationStatistics> GetByAccId(int accId) {
            return _statisticsRepository.GetAll().Where(x => x.AccommodationId == accId).ToList();
        }
        
        public bool IsStatisticEmpty(int accId) {
            return this.GetByAccId(accId).Count == 0;
        }
        
        public List<int> GetYearsWithAvailableStatistics(int accommodationId) {
            List<int> years = new List<int>();
            foreach (var statistic in _statisticsRepository.GetAll()) {
                if (!years.Contains(statistic.Year) && statistic.AccommodationId == accommodationId) {
                    years.Add(statistic.Year);
                }
            }
            years.Sort();
            return years;
        }
        
        public AccommodationStatistics GetSumOfStatisticsForYear(int accommodationId, int year) {
            AccommodationStatistics yearlyStatistic = new AccommodationStatistics(accommodationId, year, -1);

            foreach (var v in this.GetByYearForAccommodation(accommodationId, year)) {
                yearlyStatistic.DaysReserved += v.DaysReserved;
                yearlyStatistic.CancelledReservationsNum += v.CancelledReservationsNum;
                yearlyStatistic.PostponedReservationsNum += v.PostponedReservationsNum;
                yearlyStatistic.RenovationsRecommendedNum += v.RenovationsRecommendedNum;
                yearlyStatistic.ReservationsNum += v.ReservationsNum;
            }

            return yearlyStatistic;
        }

        public List<AccommodationStatistics> GetYearlyStatistics(int accommodationId) {
            List<AccommodationStatistics> list = new List<AccommodationStatistics> ();
            foreach (var year in GetYearsWithAvailableStatistics(accommodationId)) {
                list.Add(this.GetSumOfStatisticsForYear(accommodationId,year));
            }
            return list.OrderBy(x => x.Year).ToList();
        }

        public AccommodationStatistics GetBusiestYearStatistics(int accommodationId) {
            return this.GetYearlyStatistics(accommodationId).OrderByDescending(x => x.DaysReserved).FirstOrDefault();
        }

        public AccommodationStatistics GetBusiestMonthStatistics(int accommodationId, int year) {
            return this.GetByYearForAccommodation(accommodationId, year).OrderByDescending(x => x.DaysReserved).FirstOrDefault();
        }

        public int GetDaysFromFirstReservation(int accId) {
            var oldestReservation = _reservationService.GetOldestReservation(accId);
            var newestReservation = _reservationService.GetNewestReservation(accId);

            int days = this.CalculateDaysDiffrence(oldestReservation.StartDate, newestReservation.EndDate);

            return Math.Abs(days);
        }
        public void UpdatePostponedStatistics(int accId, DateOnly oldStartDate, DateOnly oldEndDate, DateOnly newStartDate, DateOnly newEndDate, int guestsNum) {
            AccommodationStatistics oldReservationStatistic = this.GetByDateForAccommodation(accId, oldStartDate.Year, oldStartDate.Month);
            oldReservationStatistic.PostponedReservationsNum++;
            this.Update(oldReservationStatistic);

            ReduceReservedDaysAndGuestsNum(accId, oldStartDate, oldEndDate , guestsNum);

            UpdateReservationStatisticsAndCheckDates(accId, newStartDate, newEndDate, guestsNum);
        }

        private void ReduceReservedDaysAndGuestsNum(int accId, DateOnly startDate, DateOnly endDate, int guestsNum) {
            Accommodation accommodation = _accommodationService.GetById(accId);
            if (startDate.Month == endDate.Month) {
                AccommodationStatistics statistic = this.GetByDateForAccommodation(accId, startDate.Year, startDate.Month);
                statistic.DaysReserved -= CalculateDaysDiffrence(startDate,endDate);
                statistic.GuestsNum -= guestsNum;
                statistic.MaximumGuestsNum -= accommodation.MaxGuestNumber;
                this.Update(statistic);
            }
            else {
                DateOnly newEndDate = new DateOnly(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
                AccommodationStatistics firstStatistic = this.GetByDateForAccommodation(accId, startDate.Year, startDate.Month);
                firstStatistic.DaysReserved -= CalculateDaysDiffrence(startDate, newEndDate);
                firstStatistic.GuestsNum -= guestsNum;
                firstStatistic.MaximumGuestsNum -= accommodation.MaxGuestNumber;

                this.Update(firstStatistic);

                DateOnly newStartDate = new DateOnly(endDate.Year, endDate.Month, 1);
                AccommodationStatistics secondStatistic = this.GetByDateForAccommodation(accId, newStartDate.Year, newStartDate.Month);
                secondStatistic.DaysReserved -= CalculateDaysDiffrence(newStartDate, endDate);
                secondStatistic.GuestsNum -= guestsNum;
                secondStatistic.MaximumGuestsNum -= accommodation.MaxGuestNumber;
                this.Update(secondStatistic);
            }
        }

        public void UpdateRecommendationStatistics(int accommodationId, DateOnly startDate) {
            AccommodationStatistics accommodationStatistics = this.GetByDateForAccommodation(accommodationId,startDate.Year,startDate.Month);
            accommodationStatistics.RenovationsRecommendedNum++;
            this.Update(accommodationStatistics);
        }

        public void UpdateCancellationStatisticsAndCheckDates(int accommodationId, DateOnly startDate, DateOnly endDate, int guestsNum) {
            if (startDate.Month == endDate.Month) {
                UpdateCancellationStatistics(accommodationId,startDate,endDate, true, guestsNum);
            }
            else {
                DateOnly newEndDate = new DateOnly(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
                UpdateCancellationStatistics(accommodationId,startDate,newEndDate, true, guestsNum);
                
                DateOnly newStartDate = new DateOnly(endDate.Year, endDate.Month, 1);
                UpdateCancellationStatistics(accommodationId,newStartDate,endDate, false, guestsNum);
            }
        }
        
        private void UpdateCancellationStatistics(int accommodationId, DateOnly startDate, DateOnly endDate, bool addCancellations, int guestsNum) {
            AccommodationStatistics statistics = this.GetByDateForAccommodation(accommodationId,startDate.Year,startDate.Month);
            Accommodation accommodation = _accommodationService.GetById(accommodationId);
            int days = this.CalculateDaysDiffrence(startDate, endDate);
            statistics.DaysReserved -= days;
            statistics.GuestsNum -= guestsNum;
            statistics.MaximumGuestsNum -= accommodation.MaxGuestNumber;
            
            if(addCancellations)
                statistics.CancelledReservationsNum++;
            this.Update(statistics);

        }
        
        public void UpdateReservationStatisticsAndCheckDates(int accommodationId, DateOnly startDate, DateOnly endDate, int guestsNum) {
            if (startDate.Month == endDate.Month) {
                UpdateReservationStatistics(accommodationId, startDate, endDate, true, guestsNum);
            }
            else {
                DateOnly newEndDate = new DateOnly(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
                UpdateReservationStatistics(accommodationId, startDate, newEndDate, true, guestsNum);

                DateOnly newStartDate = new DateOnly(endDate.Year, endDate.Month, 1);
                UpdateReservationStatistics(accommodationId, newStartDate, endDate, false, guestsNum);
            }
        }
        
        private void UpdateReservationStatistics(int accommodationId, DateOnly startDate, DateOnly endDate, bool addReservation, int guestsNum) {
            AccommodationStatistics statistic = this.GetByDateForAccommodation(accommodationId, startDate.Year, startDate.Month);
            Accommodation accommodation = _accommodationService.GetById(accommodationId);
            if (statistic == null) {
                statistic = new AccommodationStatistics(accommodationId, startDate.Year, startDate.Month);
                if (addReservation)
                    statistic.ReservationsNum++;
                statistic.DaysReserved += this.CalculateDaysDiffrence(startDate, endDate);
                statistic.GuestsNum += guestsNum;
                statistic.MaximumGuestsNum += accommodation.MaxGuestNumber;

                this.Save(statistic);
                return;
            }

            if (addReservation)
                statistic.ReservationsNum++;
            statistic.DaysReserved += this.CalculateDaysDiffrence(startDate, endDate);
            statistic.GuestsNum += guestsNum;
            statistic.MaximumGuestsNum += accommodation.MaxGuestNumber;
            this.Update(statistic);
        }
        
        private int CalculateDaysDiffrence(DateOnly startDate, DateOnly endDate) {
            DateTime dateTime1 = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            DateTime dateTime2 = new DateTime(endDate.Year, endDate.Month, endDate.Day);

            int differenceInDays = (dateTime2 - dateTime1).Days;
            return  Math.Abs(differenceInDays);
        }

        /// <summary>
        /// Retrieves the hottest and coldest locations based on the provided owner ID.
        /// </summary>
        /// <param name="ownerId">The ID of the owner.</param>
        /// <returns>A list of location IDs representing the hottest and coldest locations.
        /// First 2 elements are the hotest locations, last two are coldest, this is calculated
        /// on a simple scoring system, that takes into account two things:
        /// guest fullness precentage and number of reservations.
        /// If there are less than 5 locations, method returns one element with -1 id!
        /// If there are no statistics ,method returns one element with -2 id!
        /// </returns>
        public List<int> GetHottestAndColdestLocations(int ownerId) {
            List<int> locationIds = new List<int>();
            Dictionary<int, double> locationScores = GetScoresForLocations(ownerId);

            if (locationScores.Count < 5) {
                locationIds.Add(-1);
                return locationIds;
            }

            if (!CheckForSufficentStatistics(locationScores)) {
                locationIds.Add(-2);
                return locationIds;
            }

            int key = locationScores.MaxBy(entry => entry.Value).Key;
            locationIds.Add(key);
            locationScores.Remove(key);

            int key2 = locationScores.MaxBy(entry => entry.Value).Key;
            locationIds.Add(key2);

            int key3 = locationScores.MinBy(entry => entry.Value).Key;
            locationScores.Remove(key3);
            int key4 = locationScores.MinBy(entry => entry.Value).Key;

            locationIds.Add(key4);
            locationIds.Add(key3);

            return locationIds;
        }

        public bool CheckForSufficentStatistics(Dictionary<int, double> locationsScores) {
            int zeroCounter = 0;

            foreach (var v in locationsScores) {
                if (v.Value == 0) {
                    zeroCounter++;
                }
            }

            if (zeroCounter > 3) {
                return false;
            }

            return true;
        }

        public Dictionary<int, double> GetScoresForLocations(int ownerId) {
            double weight1 = 0.3;
            double weight2 = 0.7;
            
            Dictionary<int, double> locationScores = new Dictionary<int, double>();

            foreach (var accommodation in _accommodationService.GetByOwnerId(ownerId)) {
                if (!locationScores.ContainsKey(accommodation.LocationId))
                    locationScores.Add(accommodation.LocationId, 0); //score 0

                int sumGuestsNum = 0;
                int sumMaxiumumGuestsNum = 0;
                int sumReservationsNum = 0;

                foreach (var statistic in this.GetByAccId(accommodation.Id)) {
                    sumGuestsNum += statistic.GuestsNum;
                    sumMaxiumumGuestsNum += statistic.MaximumGuestsNum;
                    sumReservationsNum += statistic.ReservationsNum;
                }

                if (sumReservationsNum == 0)
                    continue;

                locationScores[accommodation.LocationId] += weight1 * sumGuestsNum / sumMaxiumumGuestsNum + weight2 * sumReservationsNum / 100;
            }

            return locationScores;
        }
    }
}
