using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;

namespace BookingApp.WPF.Web.ViewModels {
    public class RescheduleReservationModalDialogViewModel
    {
        public AccommodationReservationDTO Reservation { get; set; }
        public DateTime NewDate { get; set; }

        private readonly AccommodationRescheduleRequestService _rescheduleRequestService = ServicesPool.GetService<AccommodationRescheduleRequestService>();

        public RescheduleReservationModalDialogViewModel(AccommodationReservationDTO reservation) {
            Reservation = reservation;
        }

        public void Reschedule() {
            AccommodationRescheduleRequest newRequest = new AccommodationRescheduleRequest(
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
