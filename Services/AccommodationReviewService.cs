using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Windows.Documents;

namespace BookingApp.Services {
    public class AccommodationReviewService {
        
        private readonly IAccommodationReviewRepository _reviewRepository;

        private AccommodationReservationService _reservationService;
        private OwnerService _ownerService;
        private AccommodationStatisticsService _accommodationStatisticsService;
        private AccommodationService _accommodationService;
        
        public AccommodationReviewService(IAccommodationReviewRepository reviewRepository) {
            _reviewRepository = reviewRepository;
        }

        public void InjectServices(AccommodationReservationService reservationService, OwnerService ownerService, 
                AccommodationStatisticsService accommodationStatisticsService, AccommodationService accService) {
            _reservationService = reservationService;
            _ownerService = ownerService;
            _accommodationStatisticsService = accommodationStatisticsService;
            _accommodationService = accService;
        }
        
        public List<AccommodationReview> GetByGuestId(int ownerId) { 
            return _reviewRepository.GetByGuestId(ownerId);
        }

        public List<AccommodationReview> GetByOwnerId(int ownerId) {
            return _reviewRepository.GetByOwnerId(ownerId);
        }

        public AccommodationReview GetByReservationId(int reservationId) {
            return _reviewRepository.GetByReservationId(reservationId);
        }
        
        public void GradeGuest(AccommodationReviewDTO reviewDTO) {
            AccommodationReview review = this.GetByReservationId(reviewDTO.ReservationId);
            if (review == null) {
                _reviewRepository.Save(reviewDTO.ToReview());
                return;
            }

            review.GuestCleannessGrade = reviewDTO.GuestCleannessGrade;
            review.RuleFollowingGrade = reviewDTO.RuleFollowingGrade;
            review.OwnerComment = reviewDTO.OwnerComment;
            _reviewRepository.Update(review);
        }

        public void GradeOwner(AccommodationReviewDTO reviewDTO) {
            if (reviewDTO.RequiresRenovation) {
                AccommodationReservation accRes = _reservationService.GetById(reviewDTO.ReservationId);
                _accommodationStatisticsService.UpdateRecommendationStatistics(accRes.AccommodationId, accRes.StartDate); ;
            }

            AccommodationReview review = this.GetByReservationId(reviewDTO.ReservationId);
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

        public bool IsGradedByGuest(int reservationId) {
            AccommodationReview review = this.GetByReservationId(reservationId);
            if (review == null) {
                return false;
            }
            if (review.OwnerCorrectnessGrade == 0 || review.AccommodationCleannessGrade == 0) {
                return false;
            }
            return true;
        }
        
        public bool IsGradedByOwner(int reservationId) {
            AccommodationReview review = this.GetByReservationId(reservationId);
            if (review == null) {
                return false;
            }
            if (review.GuestCleannessGrade == 0 || review.RuleFollowingGrade == 0) {
                return false;
            }
            return true;
        }
        
        public Dictionary<Accommodation,double> GetAverageAccommodationGradesByOwnerId(int ownerId, AccommodationType type) {
            Dictionary<Accommodation, double> allAccommodations = new Dictionary<Accommodation, double>();

            foreach (var acc in _accommodationService.GetByOwnerId(ownerId)) {
                if (acc.Type == type) {
                    double averageGrade = this.CalculateAverageAccommodationGrade(acc.Id);
                    allAccommodations.Add(acc, averageGrade);
                }
            }

            return allAccommodations;
        }

        private double CalculateAverageAccommodationGrade(int accId) {
            double avgGrade = 0;
            int gradeCount = 0;
            foreach (var reservation in _reservationService.GetByAccommodationId(accId)) {
                AccommodationReview accReview = this.GetByReservationId(reservation.Id);
                if (accReview == null || accReview.OwnerCorrectnessGrade == 0 || accReview.AccommodationCleannessGrade == 0) {
                    continue;
                }

                double avg = (accReview.OwnerCorrectnessGrade + accReview.AccommodationCleannessGrade)/2.0;
                gradeCount++;

                avgGrade += avg;
            }

            if (avgGrade == 0 || gradeCount == 0) {
                return 0;
            }

            return avgGrade / gradeCount;
        }
    
        private List<AccommodationReview> GetByAccommodationId(int accId) {
            List<AccommodationReservation> reservations = _reservationService.GetByAccommodationId(accId);
            List<AccommodationReview> reviews = new List<AccommodationReview>();

            foreach (var res in reservations) {
                AccommodationReview review = this.GetByReservationId(res.Id);
                if (review != null) {
                    reviews.Add(review);
                }
            }

            return reviews;
        }

        public double GetAverageCleannessGrade(int accId) {
            var reviews = this.GetByAccommodationId(accId);

            if (reviews.Count == 0)
                return 0.0;

            return reviews.Average(r => r.GuestCleannessGrade);
        }

        public double GetAverageCorrectnessGrade(int accId) {
            var reviews = this.GetByAccommodationId(accId);

            if (reviews.Count == 0)
                return 0.0;

            return reviews.Average(r => r.RuleFollowingGrade);
        }
    }
}
