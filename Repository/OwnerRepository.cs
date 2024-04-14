using BookingApp.Model;
using BookingApp.RepositoryInterfaces;

namespace BookingApp.Repository {
    public class OwnerRepository : Repository<Owner>, IOwnerRepository { }
}