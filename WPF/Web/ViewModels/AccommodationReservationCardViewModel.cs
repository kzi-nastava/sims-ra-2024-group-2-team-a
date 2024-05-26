using BookingApp.Services;
using BookingApp.WPF.DTO;

namespace BookingApp.WPF.Web.ViewModels {

    public class AccommodationReservationCardViewModel {

        public AccommodationReservationDTO Reservation { get; set; }

        public AccommodationReservationCardViewModel(AccommodationReservationDTO reservation) {
            Reservation = reservation;
        }
    }
}
