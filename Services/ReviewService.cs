using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.DTO;
using System.Collections.Generic;
using System.Windows.Documents;

namespace BookingApp.Services {
    public class ReviewService {
        
        private readonly IReviewRepository _reviewRepository;

        private AccommodationReservationService _reservationService;
        private OwnerService _ownerService;
        private AccommodationStatisticsService _accommodationStatisticsService;
        
        public ReviewService(IReviewRepository reviewRepository) {
            _reviewRepository = reviewRepository;
        }

        public void InjectServices(AccommodationReservationService reservationService, OwnerService ownerService, AccommodationStatisticsService accommodationStatisticsService) {
            _reservationService = reservationService;
            _ownerService = ownerService;
            _accommodationStatisticsService = accommodationStatisticsService;
        }
        
        public List<Review> GetByGuestId(int ownerId) { 
            return _reviewRepository.GetByGuestId(ownerId);
        }

        public List<Review> GetByOwnerId(int ownerId) {
            return _reviewRepository.GetByOwnerId(ownerId);
        }

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

            if (reviewDTO.RequiresRenovation) {
                AccommodationReservation accRes = _reservationService.GetById(reviewDTO.ReservationId);
                _accommodationStatisticsService.UpdateRecommendationStatistics(accRes.AccommodationId, accRes.StartDate);;
            }
        }

        public bool IsGradedByGuest(int reservationId) {
            Review review = this.GetByReservationId(reservationId);
            if (review == null) {
                return false;
            }
            if (review.OwnerCorrectnessGrade == 0 || review.AccommodationCleannessGrade == 0) {
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
