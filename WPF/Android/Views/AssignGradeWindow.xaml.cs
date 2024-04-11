﻿using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.Android.Views;
using System.Windows;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for AssignGradeWindow.xaml
    /// </summary>
    public partial class AssignGradeWindow : Window {
        public AccommodationReservationDTO AccReservationDTO { get; set; }
        public ReviewDTO ReviewDTO { get; set; }

        private ReviewService reviewService = new ReviewService();

        private int _ownerId;

        private ReservationReviewsPage _reservationReviewsPage;
        public AssignGradeWindow(AccommodationReservationDTO accResDTO, int ownerId, ReservationReviewsPage page) {
            InitializeComponent();
            DataContext = this;

            AccReservationDTO = accResDTO;
            _ownerId = ownerId;
            ReviewDTO = new ReviewDTO();
            _reservationReviewsPage = page;
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e) {
            if(ReviewDTO.GuestCleannessGrade == 0){
                ReviewDTO.GuestCleannessGrade = 1;
            }
            if (ReviewDTO.RuleFollowingGrade == 0) {
                ReviewDTO.RuleFollowingGrade = 1;
            }

            ReviewDTO.GuestId = AccReservationDTO.GuestId;
            ReviewDTO.OwnerId = _ownerId;
            ReviewDTO.ReservationId = AccReservationDTO.Id;

            reviewService.GradeGuest(ReviewDTO);
            _reservationReviewsPage.Update();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}