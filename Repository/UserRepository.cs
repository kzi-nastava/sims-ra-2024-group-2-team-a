using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Linq;

namespace BookingApp.Repository {
    public class UserRepository : Repository<User>, IUserRepository {

        public User GetByUsername(string username) {
            return this.GetAll().FirstOrDefault(x => x.Username == username);
        }
    }
}
