using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BookingApp.Repository {
    public class AccommodationRepository : Repository<Accommodation>, IAccommodationRepository {

        public List<Accommodation> GetByOwnerId(int ownerId) {
            _items = _serializer.FromCSV();

            return _items.Where(a => a.OwnerId == ownerId).ToList();
        }
    }
}