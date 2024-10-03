using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.DTO
{
    public class AccommodationReservationFilterDTO
    {
        public int AccommodationId { get; set; } = 0;
        public int ReservationDays { get; set; } = 0;
        public int GuestsNumber { get; set; } = 0;
        public DateOnly StartDate { get; set; } = new DateOnly();
        public DateOnly EndDate { get; set; } = new DateOnly();

        public AccommodationReservationFilterDTO() { }

        public AccommodationReservationFilterDTO(int accommodationId, int reservationDays, int guestsNumber, DateOnly startDate, DateOnly endDate) {
            AccommodationId = accommodationId;
            ReservationDays = reservationDays;
            GuestsNumber = guestsNumber;
            StartDate = startDate;
            EndDate = endDate;
        }

        public AccommodationReservationFilterDTO(AccommodationReservationFilterDTO filter) {
            AccommodationId = filter.AccommodationId;
            ReservationDays = filter.ReservationDays;
            GuestsNumber = filter.GuestsNumber;
            StartDate = filter.StartDate;
            EndDate = filter.EndDate;
        }
    }
}
