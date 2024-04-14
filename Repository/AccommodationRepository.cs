using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {
    public class AccommodationRepository : Repository<Accommodation>, IAccommodationRepository {

        public List<Accommodation> GetByOwnerId(int ownerId) {
            _items = _serializer.FromCSV();

            return _items.Where(a => a.OwnerId == ownerId).ToList();
        }
    }
}