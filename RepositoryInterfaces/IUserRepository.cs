using BookingApp.Model;

namespace BookingApp.RepositoryInterfaces {
    public interface IUserRepository : IRepository<User> {
        User GetByUsername(string username);
    }
}
