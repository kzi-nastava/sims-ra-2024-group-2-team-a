using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Repository {
    public class LocationRepository : Repository<Location>, ILocationRepository { }
}
