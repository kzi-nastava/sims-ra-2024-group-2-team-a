using BookingApp.Model;
using BookingApp.RepositoryInterfaces;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {
    public class UserRepository : Repository<User>, IUserRepository {

        public User GetByUsername(string username) {
            return this.GetAll().FirstOrDefault(x => x.Username == username);
        }
    }
}
