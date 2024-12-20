﻿using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.DTO;

namespace BookingApp.WPF.Android.ViewModels {
    public class ViewGuestGradeViewmodel {

        public AccommodationReservationDTO AccommodationReservationDTO { get; set; }
        public AccommodationReviewDTO ReviewDTO { get; set; }
        public string GuestUsername { get; set; }

        private AccommodationReviewService reviewService = ServicesPool.GetService<AccommodationReviewService>();

        private readonly UserService _userService;

        public ViewGuestGradeViewmodel(AccommodationReservationDTO selectedReservationDTO) {
            _userService = ServicesPool.GetService<UserService>();

            AccommodationReservationDTO = selectedReservationDTO;
            ReviewDTO = new AccommodationReviewDTO(reviewService.GetByReservationId(selectedReservationDTO.Id));
            GuestUsername = _userService.GetById(selectedReservationDTO.GuestId).Username;
        }
    }
}
