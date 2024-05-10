using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System.Collections.Generic;

namespace BookingApp.Services {

    public class OwnerService {

        private readonly IOwnerRepository _ownerRepository;

        private ReviewService _reviewService;

        public OwnerService(IOwnerRepository ownerRepository) {
            _ownerRepository = ownerRepository;
        }

        public void InjectServices(ReviewService reviewService) {
            _reviewService = reviewService;
        }

        public Owner GetByUserId(int userId) {
            return _ownerRepository.GetAll().Find(owner => owner.UserId == userId);
        }

        public Owner Save(Owner owner) {
            return _ownerRepository.Save(owner);
        }

        public void AdjustSuperOwner(int ownerId) {
            Owner owner = this.GetByUserId(ownerId);

            int numberOfReviews = this.CalculateOwnerAverageGrade(owner);

            bool oldSuper = owner.IsSuper;

            if (numberOfReviews >= 50 && owner.AverageGrade >= 4.5)
                owner.IsSuper = true;
            else
                owner.IsSuper = false;

            if (!oldSuper && owner.IsSuper) {
                NotificationService notificationService = ServicesPool.GetService<NotificationService>();
                notificationService.CreateSuperNotification(owner.UserId);
            }

            _ownerRepository.Update(owner);
        }

        private int CalculateOwnerAverageGrade(Owner owner) {
            List<AccommodationReview> reviews = _reviewService.GetByOwnerId(owner.UserId);

            double sum = 0;
            int numberOfReviews = 0;
            foreach (AccommodationReview review in reviews) {
                if (!IsOwnerGraded(review)) {
                    continue;
                }

                double average = (review.OwnerCorrectnessGrade + review.AccommodationCleannessGrade) / 2.0;
                sum += average;
                numberOfReviews++;
            }

            owner.AverageGrade = sum / numberOfReviews;

            return numberOfReviews;
        }

        private bool IsOwnerGraded(AccommodationReview review) {
            if (review.OwnerCorrectnessGrade != 0 && review.AccommodationCleannessGrade != 0) {
                return true;
            }
            return false;
        }
    }

}
