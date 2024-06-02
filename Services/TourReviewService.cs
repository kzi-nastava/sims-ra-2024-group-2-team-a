using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services {
    public class TourReviewService {
        private readonly ITourReviewRepository _tourReviewRepository;
        public TourReviewService(ITourReviewRepository tourReviewRepository) {
            _tourReviewRepository = tourReviewRepository;
        }

        public List<TourReview> GetAll() {
            return _tourReviewRepository.GetAll();
        }

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

        private List<TourReview> GetByTours(List<Tour> tours) {
            return _tourReviewRepository.GetByTours(tours);
        }

        public double GetAvrageScore(List<Tour> tours) {
            /*double sumScore = 0;
            foreach(Tour tour in tours) {
                List<TourReview> reviews = GetByTourId(tour.Id);
                sumScore += reviews.Average(x => x.AvrageGrade);
            }
            return sumScore / tours.Count();*/

            return GetByTours(tours).Average(x => x.AvrageGrade);
        }
    }
}
