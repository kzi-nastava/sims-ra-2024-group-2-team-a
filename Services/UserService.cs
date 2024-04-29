using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Services {
    public class UserService {

        private readonly IUserRepository _userRepository = RepositoryInjector.GetInstance<IUserRepository>();

        private readonly OwnerService _ownerService = new OwnerService();
        private readonly GuestService _guestService = new GuestService();

        public User GetByUsername(string username) {
            return _userRepository.GetByUsername(username);
        }

        public User GetById(int id) {
            return _userRepository.GetById(id);
        }

        public User Save(User user) {
            var savedUser = _userRepository.Save(user);

            if (savedUser.Category == UserCategory.Owner) {
                Owner owner = new Owner();
                owner.UserId = savedUser.Id;
                _ownerService.Save(owner);
            }
            else if (savedUser.Category == UserCategory.Guest) {
                Guest guest = new Guest(savedUser);
                _guestService.Save(guest);
            }

            return savedUser;
        }
    }
}
