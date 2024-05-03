using BookingApp.Services;
using BookingApp.WPF.DTO;

namespace BookingApp.WPF.Web.ViewModels {

    public class AccommodationReservationCardViewModel {

        public AccommodationReservationDTO Reservation { get; set; }

        private readonly AccommodationReservationService _reservationService = ServicesPool.GetService<AccommodationReservationService>();

        public AccommodationReservationCardViewModel(AccommodationReservationDTO reservation) {
            Reservation = reservation;
        }

        public void CancelReservation() {
            _reservationService.CancelReservation(Reservation.Id);
        }
    }
}
