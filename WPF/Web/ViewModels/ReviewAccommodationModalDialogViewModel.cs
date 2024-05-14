using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;

namespace BookingApp.WPF.Web.ViewModels {

    public class ReviewAccommodationModalDialogViewModel {

        public AccommodationReservationDTO Reservation { get; set; }
        public ImportanceType[] ImportanceTypes { get; set; } = (ImportanceType[])Enum.GetValues(typeof(ImportanceType));
        public AccommodationReviewDTO Review { get; set; } = new AccommodationReviewDTO();

        private readonly AccommodationReviewService _reviewService = ServicesPool.GetService<AccommodationReviewService>();

        public ReviewAccommodationModalDialogViewModel(AccommodationReservationDTO reservation) {
            Reservation = reservation;

            Review.ReservationId = Reservation.Id;
            Review.GuestId = Reservation.GuestId;
            Review.OwnerId = Reservation.Accommodation.OwnerId;
        }

        public void GradeOwner() {
            _reviewService.GradeOwner(Review);
        }
    }
}
