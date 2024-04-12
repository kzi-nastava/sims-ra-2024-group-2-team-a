using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Web.ViewModels
{
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
