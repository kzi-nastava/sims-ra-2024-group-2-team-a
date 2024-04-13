﻿using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.Services {
    public class TourService {
        private readonly TourRepository _tourRepository = new TourRepository();
        private readonly PointOfInterestRepository _pointOfInterestRepository = new PointOfInterestRepository();
        private readonly PassengerRepository _passengerRepository = new PassengerRepository();
        private readonly TourReviewRepository _tourReviewRepository = new TourReviewRepository();

        private readonly PassengerService _passengerService = new PassengerService();
        private readonly TourReservationService _tourReservationService = new TourReservationService();


        public List<Passenger> GetTouristEntries(int touristId) {
            return _passengerRepository.GetAll().Where(p => p.UserId == touristId).ToList();
        }

        public bool WasTouristPresent(int touristId, TourDTO tour) {
            List<PointOfInterest> checkpoints = _pointOfInterestRepository.GetAllByTourId(tour.Id);

            foreach (var tourist in GetTouristEntries(touristId)) {
                if (checkpoints.Any(c => c.Id == tourist.JoinedPointOfInterestId))
                    return true;
            }

            return false;
        }

        public bool IsTourReviewedForUser(int userId, TourDTO tour) {
            return _tourReviewRepository.GetAll().Find(t => t.TourId == tour.Id && t.TouristId == userId) != null;
        }

        public List<int> GetTourStats(int tourId) {
            return _passengerService.GetAttendance(_tourReservationService.GetByTourId(tourId));
        }
        public List<Tour> GetScheduled(int userId) {
            return _tourRepository.GetScheduled(userId);
        }
        public List<Tour> GetFinished(int userId) {
            return _tourRepository.GetFinished(userId);
        }
        public List<Tour> GetLive(int userId) {
            return _tourRepository.GetLive(userId);
        }
        public Tour GetMostViewedByYear(int year) {
            List<Tour> tours = _tourRepository.GetByYear(year);
            if (tours == null)
                return null;
            return tours.MaxBy(y => y.CurrentTouristNumber);
        }
        public List<Tour> GetFilteredScheduled(TourFilterDTO filter, int userId) {
            List<Tour> tours = GetScheduled(userId);

            if (filter.isEmpty())
                return tours;

            return tours.Where(x => IsFilteredScheduled(x, filter)).ToList();
        }
        public List<Tour> GetFilteredLive(TourFilterDTO filter, int userId) {
            List<Tour> tours = GetLive(userId);

            if (filter.isEmpty())
                return tours;

            return tours.Where(x => IsFilteredLive(x, filter)).ToList();
        }
        private bool IsFilteredScheduled(Tour tour, TourFilterDTO filter) {
            return _tourRepository.MatchesName(tour, filter) &&
                   _tourRepository.MatchesLocation(tour, filter) &&
                   _tourRepository.MatchesDuration(tour, filter) &&
                   _tourRepository.MatchesLanguage(tour, filter) &&
                   _tourRepository.MatchesCurrentTouristNumber(tour, filter) &&
                   _tourRepository.MatchesDate(tour, filter);
        }
        private bool IsFilteredLive(Tour tour, TourFilterDTO filter) {
            return _tourRepository.MatchesName(tour, filter) &&
                   _tourRepository.MatchesLocation(tour, filter) &&
                   _tourRepository.MatchesDuration(tour, filter) &&
                   _tourRepository.MatchesLanguage(tour, filter) &&
                   _tourRepository.MatchesCurrentTouristNumber(tour, filter);
        }
        public Tour Save(Tour tour) {
            return _tourRepository.Save(tour);
        }
        public bool Delete(Tour tour) => _tourRepository.Delete(tour);
        public bool Update(Tour tour) {
            return _tourRepository.Update(tour);
        }
    }
}
