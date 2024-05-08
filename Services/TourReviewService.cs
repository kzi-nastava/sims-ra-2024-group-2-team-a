﻿using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.DTO;
using System.Collections.Generic;

namespace BookingApp.Services {
    public class TourReviewService {
        private readonly ITourReviewRepository _tourReviewRepository;
        public TourReviewService(ITourReviewRepository tourReviewRepository) {
            _tourReviewRepository = tourReviewRepository;
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
    }
}
