using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Repository {
    public class OwnerRepository : Repository<Owner>, IOwnerRepository { }
}