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
    }
}
