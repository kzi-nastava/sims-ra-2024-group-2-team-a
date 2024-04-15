using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.DTO;

namespace BookingApp.WPF.Android.ViewModels {
    public class ViewGuestGradeViewmodel {

        public AccommodationReservationDTO AccommodationReservationDTO { get; set; }
        public ReviewDTO ReviewDTO { get; set; }
        public string GuestUsername { get; set; }

        private ReviewService reviewService = new ReviewService();

        private readonly UserService _userService;

        public ViewGuestGradeViewmodel(AccommodationReservationDTO selectedReservationDTO) {
            _userService = new UserService();

            AccommodationReservationDTO = selectedReservationDTO;
            ReviewDTO = new ReviewDTO(reviewService.GetByReservationId(selectedReservationDTO.Id));
            GuestUsername = _userService.GetById(selectedReservationDTO.GuestId).Username;
        }
    }
}
