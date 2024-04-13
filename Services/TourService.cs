using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TourService {
        private readonly TourRepository _tourRepository = new TourRepository();
        private readonly PointOfInterestRepository _pointOfInterestRepository = new PointOfInterestRepository();
        private readonly PassengerRepository _passengerRepository = new PassengerRepository();
        private readonly TourReviewRepository _tourReviewRepository = new TourReviewRepository();

        private bool IsFiltered(Tour tour, TourFilterDTO filter) {
            return MatchesLocation(tour, filter) &&
                   MatchesDuration(tour, filter) &&
                   MatchesLanguage(tour, filter) &&
                   MatchesMaxTouristNumber(tour, filter);
        }

        private bool MatchesLocation(Tour tour, TourFilterDTO filter) {
            return tour.LocationId == filter.Location.Id || filter.Location.Id == -1;
        }

        private bool MatchesDuration(Tour tour, TourFilterDTO filter) {
            return tour.Duration <= filter.Duration || filter.Duration == 0;
        }

        private bool MatchesLanguage(Tour tour, TourFilterDTO filter) {
            return tour.LanguageId == filter.Language.Id || filter.Language.Id == -1;
        }

        private bool MatchesMaxTouristNumber(Tour tour, TourFilterDTO filter) {
            return tour.MaxTouristNumber >= filter.TouristNumber || filter.TouristNumber == 0;
        }

        public List<Tour> GetFiltered(TourFilterDTO filter) {
            List<Tour> allTours = _tourRepository.GetAll();

            if (filter.isEmpty())
                return allTours;

            return allTours.Where(t => IsFiltered(t, filter)).ToList();
        }

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

        public List<Tour> GetToursByLocation(int locationId) {
            return _tourRepository.GetAll().Where(t => (locationId == t.LocationId)).ToList();
        }

        public List<Tour> GetSameLocationTours(TourDTO tour) {
            return GetToursByLocation(tour.LocationId).Where(t => (tour.Id != t.Id)).ToList();
        }

        public int GetAvailableSpace(TourDTO tour) {
            return tour.MaxTouristNumber - tour.CurrentTouristNumber;
        }
    }
}
