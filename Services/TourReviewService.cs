using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class TourReviewService {
        private readonly TourReviewRepository _tourReviewRepository = new TourReviewRepository();

        public void SendReview(TourDTO tour, int touristId, TourReviewDTO tourReviewDTO) {
            tourReviewDTO.TourId = tour.Id;
            tourReviewDTO.TouristId = touristId;
            _tourReviewRepository.Save(new TourReview(tourReviewDTO));
        }
        public List<TourReview> GetByTourId(int tourId) {
            return _tourReviewRepository.GetByTourId(tourId);
        }
        public bool Update(TourReview tourReview) {
            return _tourReviewRepository.Update(tourReview);
        }
    }
}
