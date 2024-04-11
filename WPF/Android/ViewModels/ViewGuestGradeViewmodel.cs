using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
