using BookingApp.DTO;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {

    public class ReviewRepository : Repository<Review> {

        override public Review Save(Review item) {
            Review review = this.GetById(item.Id);
            if(review == null) {
                return base.Save(item);
            }

            base.Update(item);
            return item;
        }

        public Review GetByReservationId(int id) {
            return _serializer.FromCSV().Find(x => x.ReservationId == id);
        }

        public List<Review> GetByOwnerId(int ownerId) {
            return _serializer.FromCSV().Where(review => review.OwnerId == ownerId).ToList();
        }
        
        public void GradeOwner(ReviewDTO reviewDTO) {

            if(!OwnerCanBeGraded(reviewDTO)) {
                return;
            }

            Review review = this.GetByReservationId(reviewDTO.ReservationId);
            if (review == null) {
                review = new Review(reviewDTO.ReservationId, reviewDTO.GuestId, reviewDTO.OwnerId);
            }
            
            review.AccommodationCleannessGrade = reviewDTO.AccommodationCleannessGrade;
            review.OwnerCorrectnessGrade = reviewDTO.OwnerCorrectnessGrade;

            this.Save(review);

            OwnerRepository ownerRepository = new OwnerRepository();
            ownerRepository.AdjustSuperOwner(reviewDTO.OwnerId);
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

        public bool OwnerCanBeGraded(ReviewDTO reviewDTO) {
            AccommodationReservationRepository accommodationReservationRepository = new AccommodationReservationRepository();
            AccommodationReservation reservation = accommodationReservationRepository.GetById(reviewDTO.ReservationId);

            DateOnly nowDate = DateOnly.FromDateTime(System.DateTime.Now);
            if(reservation.EndDate.AddDays(5) < nowDate) {
                return false;
            }

            return true;
        }
    }
}
