using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    class LocationService
    {
        private readonly LocationRepository _locationRepository = new LocationRepository();

        public List<Location> GetAll() {
            return _locationRepository.GetAll();
        }

        public Location GetById(int id) {
            return _locationRepository.GetById(id);
        }
    }
}
