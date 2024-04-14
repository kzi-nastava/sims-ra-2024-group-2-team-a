using BookingApp.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;

namespace BookingApp.WPF.Web.ViewModels {
    public class RescheduleReservationModalDialogViewModel
    {
        public AccommodationReservationDTO Reservation { get; set; }
        public DateTime NewDate { get; set; }

        private readonly RescheduleRequestService _rescheduleRequestService = new RescheduleRequestService();

        public RescheduleReservationModalDialogViewModel(AccommodationReservationDTO reservation) {
            Reservation = reservation;
        }

        public void Reschedule() {
            RescheduleRequest newRequest = new RescheduleRequest(
                RescheduleRequestStatus.Pending,
                Reservation.Id,
                Reservation.GuestId,
                Reservation.Accommodation.OwnerId,
                Reservation.StartDate,
                DateOnly.FromDateTime(NewDate),
                Reservation.ReservationDays,
                "");

            _rescheduleRequestService.Save(newRequest);
        }
    }
}
