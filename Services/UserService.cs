using BookingApp.Model;
using BookingApp.RepositoryInterfaces;

namespace BookingApp.Services {
    public class UserService {

        private readonly IUserRepository _userRepository = RepositoryInjector.GetInstance<IUserRepository>();
        private readonly OwnerService _ownerService = new OwnerService();

        public User GetByUsername(string username) {
            return _userRepository.GetByUsername(username);
        }

        public User Save(User user) {
            var savedUser = _userRepository.Save(user);

            if (savedUser.Category == UserCategory.Owner) {
                Owner owner = new Owner();
                owner.UserId = savedUser.Id;
                _ownerService.Save(owner);
            }

            return savedUser;
        }
    }
}
