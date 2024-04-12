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

        public bool WasTouristPresent(int touristId, TourDTO tour) {
            List<PointOfInterest> checkpoints = _pointOfInterestRepository.GetAllByTourId(tour.Id);
            List<Passenger> touristEntries = _passengerRepository.GetAll().Where(p => p.UserId == touristId).ToList();

            foreach (var tourist in touristEntries) {
                if (checkpoints.Any(c => c.Id == tourist.JoinedPointOfInterestId))
                    return true;
            }

            return false;
        }
    }
}
