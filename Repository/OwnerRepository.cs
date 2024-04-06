﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;
using BookingApp.Serializer;

namespace BookingApp.Repository {
    public class OwnerRepository : Repository<Owner> {
        
        /*public Owner GetByUserId(int userId) {
            return _serializer.FromCSV().Find(owner => owner.UserId == userId);
        }*/

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

            Owner owner = this.GetById(ownerId);
            owner.AverageGrade = sum / numberOfReviews;

            bool oldSuper = owner.IsSuper;

            if (numberOfReviews > 50 && owner.AverageGrade > 4.5)
                owner.IsSuper = true;
            else
                owner.IsSuper = false;

            if (!oldSuper && owner.IsSuper) {
                //posalji notifikaciju:
                //Postali ste Super Vlasnik!
            }

            this.Update(owner);
        }

        private bool IsOwnerGraded(Review review) {
            if (review.OwnerCorrectnessGrade != 0 && review.AccommodationCleannessGrade != 0) {
                return true;
            }
            return false;
        }
    }
}