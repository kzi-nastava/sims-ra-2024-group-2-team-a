using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class TouristService {
        private readonly ITouristRepository _touristRepository;
        public TouristService() { }
        public TouristService(ITouristRepository touristRepository) {
            _touristRepository = touristRepository;
        }

        public Tourist GetById(int id) {
            return _touristRepository.GetById(id);
        }

        public bool Update(int id, string name, string surname, DateOnly dateOfBirth) {
            return _touristRepository.Update(new Tourist(id, name, surname, dateOfBirth));
        }
    }
}
