using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces {
    public interface IAccommodationRepository : IRepository<Accommodation>
    {
        public List<Accommodation> GetByOwnerId(int ownerId);
    }
}
