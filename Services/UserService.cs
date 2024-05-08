﻿using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Services {
    public class UserService {

        private readonly IUserRepository _userRepository = RepositoryInjector.GetInstance<IUserRepository>();

        private OwnerService _ownerService;
        private GuestService _guestService;

        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public void InjectServices(OwnerService ownerService, GuestService guestService) {
            _ownerService = ownerService;
            _guestService = guestService;
        }

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
