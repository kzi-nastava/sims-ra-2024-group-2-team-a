using BookingApp.Model;
using System.Collections.Generic;

namespace BookingApp.RepositoryInterfaces {
    public interface IAccommodationRepository : IRepository<Accommodation> {
        public List<Accommodation> GetByOwnerId(int ownerId);
    }
}
