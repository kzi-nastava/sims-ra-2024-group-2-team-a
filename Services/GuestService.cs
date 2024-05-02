﻿using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class GuestService {
        
        private readonly IGuestRepository _guestRepository = RepositoryInjector.GetInstance<IGuestRepository>();

        public Guest GetById(int id) {
            return _guestRepository.GetById(id);
        }

        public Guest Save(Guest guest) {
            return _guestRepository.Save(guest);
        }

        public void PromoteToSuperGuest(int guestId) {
            Guest guest = _guestRepository.GetById(guestId);
            guest.IsSuperGuest = true;
            guest.SuperGuestExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1));
            guest.BonusPoints = Guest.SuperGuestStartPoints;
            _guestRepository.Update(guest);
        }

        public void DecrementGuestPoints(int guestId) {
            Guest guest = _guestRepository.GetById(guestId);
            guest.BonusPoints = Math.Max(0, guest.BonusPoints - 1);
            _guestRepository.Update(guest);
        }
    }
}