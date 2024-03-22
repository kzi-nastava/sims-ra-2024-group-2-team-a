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
        override public User Save(User item) {
            item.Id = NextId();
            _items = _serializer.FromCSV();
            _items.Add(item);
            _serializer.ToCSV(_items);

            if (item.Category == UserCategory.Owner) {
                OwnerRepository ownerRepository = new OwnerRepository();
                Owner owner = new Owner();
                owner.UserId = item.Id;
                ownerRepository.Save(owner);
            }
            return item;
        }
    }
}
