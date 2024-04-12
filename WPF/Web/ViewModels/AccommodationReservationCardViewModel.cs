using BookingApp.DTO;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Web.ViewModels {

    public class AccommodationReservationCardViewModel {

        public AccommodationReservationDTO Reservation { get; set; }

        private readonly AccommodationReservationService _reservationService = new AccommodationReservationService();

        public AccommodationReservationCardViewModel(AccommodationReservationDTO reservation) {
            Reservation = reservation;
        }

        public void CancelReservation() {
            _reservationService.CancelReservation(Reservation.Id);
        }
    }
}
