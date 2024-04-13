using BookingApp.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {
    public class UserRepository : Repository<User> {

        public User GetByUsername(string username) {
            _items = GetAll();
            return _items.FirstOrDefault(x => x.Username == username);
        }
    }
}
