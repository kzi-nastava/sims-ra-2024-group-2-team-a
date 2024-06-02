using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class GuestService {

        private readonly IGuestRepository _guestRepository;

        public GuestService(IGuestRepository guestRepository) {
            _guestRepository = guestRepository;
        }

        public Guest GetById(int id) {
            Guest guest = _guestRepository.GetById(id);

            DateOnly nowDate = DateOnly.FromDateTime(DateTime.Now);

            if(nowDate > guest.SuperGuestExpirationDate) {
                DemoteSuperGuest(guest);
            }

            return guest;
        }

        public Guest Save(Guest guest) {
            return _guestRepository.Save(guest);
        }

        public void ManageGuestStatus(int guestId, int reservationsCount) {
            Guest guest = _guestRepository.GetById(guestId);

            if (guest.IsSuper) {
                this.DecrementGuestPoints(guest);
                return;
            }

            if (reservationsCount >= Guest.SuperGuestReservationsCount) {
                this.PromoteToSuperGuest(guest);
                return;
            }
        }

        public void PromoteToSuperGuest(Guest guest) {
            guest.IsSuper = true;
            guest.SuperGuestExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1));
            guest.BonusPoints = Guest.SuperGuestStartPoints;
            _guestRepository.Update(guest);
        }

        public void DemoteSuperGuest(Guest guest) {
            guest.IsSuper = false;
            guest.SuperGuestExpirationDate = new DateOnly();
            guest.BonusPoints = 0;
            _guestRepository.Update(guest);
        }

        public void DecrementGuestPoints(Guest guest) {
            guest.BonusPoints = Math.Max(0, guest.BonusPoints - 1);
            _guestRepository.Update(guest);
        }
    }
}
