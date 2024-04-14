using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
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
