using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class RequestPassengerService {
        private readonly IRequestPassengerRepository _passengerRepository;
        public RequestPassengerService(IRequestPassengerRepository requestPassengerRepository) { 
            _passengerRepository = requestPassengerRepository;
        }

        public void Save(RequestPassenger passenger) {
            _passengerRepository.Save(passenger);
        }

        public IEnumerable<RequestPassenger> GetByRequestId(int id) {
            return _passengerRepository.GetByRequestId(id);
        }
    }
}
