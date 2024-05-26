using BookingApp.Domain.Model;

namespace BookingApp.Domain.RepositoryInterfaces {
    public interface IUserRepository : IRepository<User>
    {
        User GetByUsername(string username);
    }
}
