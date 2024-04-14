using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.Generic;

namespace BookingApp.Services {
    class LocationService
    {
        private readonly ILocationRepository _locationRepository = RepositoryInjector.GetInstance<ILocationRepository>();

        public List<Location> GetAll() {
            return _locationRepository.GetAll();
        }

        public Location GetById(int id) {
            return _locationRepository.GetById(id);
        }
    }
}
