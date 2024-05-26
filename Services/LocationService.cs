using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.Generic;

namespace BookingApp.Services {
    public class LocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository) {
            _locationRepository = locationRepository;
        }

        public List<Location> GetAll() {
            return _locationRepository.GetAll();
        }

        public Location GetById(int id) {
            return _locationRepository.GetById(id);
        }
    }
}
