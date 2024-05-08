using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Web.ViewModels {
    public class ConfirmReservationCancelModalDialogViewModel {

        public AccommodationReservationDTO Reservation { get; set; }

        private readonly AccommodationReservationService _reservationService = ServicesPool.GetService<AccommodationReservationService>();

        public ConfirmReservationCancelModalDialogViewModel(AccommodationReservationDTO reservation) {
            Reservation = reservation;
        }

        public void CancelReservation() {
            _reservationService.CancelReservation(Reservation.Id);
        }
    }
}
