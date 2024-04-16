using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class AccommodationRenovationService {
        private readonly IAccommodationRenovationRepository _renovationRepository =
            RepositoryInjector.GetInstance<IAccommodationRenovationRepository>();

        public AccommodationRenovation GetById(int id) {
            return _renovationRepository.GetById(id);
        }
    }
}
