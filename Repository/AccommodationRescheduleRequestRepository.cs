using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.Generic;

namespace BookingApp.Repository {

    public class AccommodationRescheduleRequestRepository : Repository<AccommodationRescheduleRequest>, IAccommodationRescheduleRequestRepository {

        public List<AccommodationRescheduleRequest> GetPendingRequestsByOwnerId(int ownerId) {
            return this.GetAll().FindAll(x => x.OwnerId == ownerId && x.Status == RescheduleRequestStatus.Pending);
        }

        public List<AccommodationRescheduleRequest> GetByReservationId(int reservationId) {
            return this.GetAll().FindAll(x => x.ReservationId == reservationId);
        }

        public List<AccommodationRescheduleRequest> GetByGuestId(int guestId) {
            return this.GetAll().FindAll(x => x.GuestId == guestId);
        }
    }
}
