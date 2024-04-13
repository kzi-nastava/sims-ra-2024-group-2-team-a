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
            ReviewRepository reviewRepository = new ReviewRepository();
            List<Review> reviews = reviewRepository.GetByOwnerId(ownerId);

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

            Owner owner = this.GetByUserId(ownerId);
            owner.AverageGrade = sum / numberOfReviews;

            bool oldSuper = owner.IsSuper;

            if (numberOfReviews >= 50 && owner.AverageGrade > 4.5)
                owner.IsSuper = true;
            else
                owner.IsSuper = false;

            if (!oldSuper && owner.IsSuper) {
                NotificationService notificationService = new NotificationService();
                string message = $"CONGRATULATIONS!! You have just become SUPER owner!!";
                Notification notification = new Notification(message, NotificationCategory.SuperOwner, owner.UserId, DateTime.Now, false);
                notificationService.Save(notification);
            }

            _ownerRepository.Update(owner);
        }

        private bool IsOwnerGraded(Review review) {
            if (review.OwnerCorrectnessGrade != 0 && review.AccommodationCleannessGrade != 0) {
                return true;
            }
            return false;
        }
    }

}
