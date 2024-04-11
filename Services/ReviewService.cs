using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class ReviewService {
        
        private readonly ReviewRepository _reviewRepository = new ReviewRepository();
        private readonly OwnerService _ownerService = new OwnerService();
        
        public ReviewService() { }
        
        public Review GetByReservationId(int reservationId) {
            return _reviewRepository.GetByReservationId(reservationId);
        }
        
        public void GradeGuest(ReviewDTO reviewDTO) {
            Review review = this.GetByReservationId(reviewDTO.ReservationId);
            if (review == null) {
                _reviewRepository.Save(reviewDTO.ToReview());
                return;
            }

            review.GuestCleannessGrade = reviewDTO.GuestCleannessGrade;
            review.RuleFollowingGrade = reviewDTO.RuleFollowingGrade;
            review.OwnerComment = reviewDTO.OwnerComment;
            _reviewRepository.Update(review);
        }

        public void GradeOwner(ReviewDTO reviewDTO) {
            Review review = this.GetByReservationId(reviewDTO.ReservationId);
            if (review == null) {
                _reviewRepository.Save(reviewDTO.ToReview());
                _ownerService.AdjustSuperOwner(reviewDTO.OwnerId);
                return;
            }

            review.AccommodationCleannessGrade = reviewDTO.AccommodationCleannessGrade;
            review.OwnerCorrectnessGrade = reviewDTO.OwnerCorrectnessGrade;
            review.GuestComment = reviewDTO.GuestComment;
            review.RequiresRenovation = reviewDTO.RequiresRenovation;
            review.Importance = reviewDTO.Importance;
            review.RenovationComment = reviewDTO.RenovationComment;
            _reviewRepository.Update(review);
            _ownerService.AdjustSuperOwner(review.OwnerId);
        }

        public bool IsGuestGraded(int reservationId) {
            Review review = this.GetByReservationId(reservationId);
            if (review == null) {
                return false;
            }
            if (review.GuestCleannessGrade == 0 || review.RuleFollowingGrade == 0) {
                return false;
            }
            return true;
        }
        
        public bool IsGradedByOwner(int reservationId) {
            Review review = this.GetByReservationId(reservationId);
            if (review == null) {
                return false;
            }
            if (review.GuestCleannessGrade == 0 || review.RuleFollowingGrade == 0) {
                return false;
            }
            return true;
        }

    }
}
