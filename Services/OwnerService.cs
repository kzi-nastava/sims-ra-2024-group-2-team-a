using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {

    public class OwnerService {

        private readonly OwnerRepository _ownerRepository = new OwnerRepository();

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
                NotificationService notificationService = new NotificationService();
                notificationService.CreateSuperNotification(owner.UserId);
            }

            _ownerRepository.Update(owner);
        }

        private int CalculateOwnerAverageGrade(Owner owner) {
            ReviewRepository reviewRepository = new ReviewRepository();
            List<Review> reviews = reviewRepository.GetByOwnerId(owner.UserId);

            double sum = 0;
            int numberOfReviews = 0;
            foreach (Review review in reviews) {
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

        private bool IsOwnerGraded(Review review) {
            if (review.OwnerCorrectnessGrade != 0 && review.AccommodationCleannessGrade != 0) {
                return true;
            }
            return false;
        }
    }

}
