using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Services;
using System;

namespace BookingApp.WPF.Web.ViewModels {

    public class ReviewAccommodationModalDialogViewModel {

        public AccommodationReservationDTO Reservation { get; set; }
        public ImportanceType[] ImportanceTypes { get; set; } = (ImportanceType[])Enum.GetValues(typeof(ImportanceType));
        public ReviewDTO Review { get; set; } = new ReviewDTO();

        private readonly ReviewService _reviewService = new ReviewService();

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
