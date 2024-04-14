using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.DTO;

namespace BookingApp.WPF.Android.ViewModels {
    public class ViewGuestGradeViewmodel {

        public AccommodationReservationDTO AccommodationReservationDTO { get; set; }
        public ReviewDTO ReviewDTO { get; set; }
        public string GuestUsername { get; set; }

        private ReviewService reviewService = new ReviewService();

        private readonly UserRepository _userRepository;

        public ViewGuestGradeViewmodel(AccommodationReservationDTO selectedReservationDTO) {
            _userRepository = new UserRepository();

            AccommodationReservationDTO = selectedReservationDTO;
            ReviewDTO = new ReviewDTO(reviewService.GetByReservationId(selectedReservationDTO.Id));
            GuestUsername = _userRepository.GetById(selectedReservationDTO.GuestId).Username;
        }
    }
}
