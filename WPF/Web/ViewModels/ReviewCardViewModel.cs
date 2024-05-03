using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Web.ViewModels {
    public class ReviewCardViewModel {

        public AccommodationReservationDTO Reservation { get; set; }
        public ReviewDTO Review { get; set; }

        public bool IsReviewHidden { get; set; } = false;

        private LocationService _locationService = new LocationService();
        private AccommodationService _accommodationService = new AccommodationService();
        private AccommodationReservationService _reservationService = new AccommodationReservationService();

        public ReviewCardViewModel(ReviewDTO review) {
            Review = review;
            Reservation = new AccommodationReservationDTO(_reservationService.GetById(review.ReservationId));
            Reservation.Accommodation = new AccommodationDTO(_accommodationService.GetById(Reservation.AccommodationId));
            Reservation.Accommodation.Location = new LocationDTO(_locationService.GetById(Reservation.Accommodation.LocationId));

            IsReviewHidden = !Review.IsGradedByGuest;
        }
    }
}
