﻿using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.Generic;

namespace BookingApp.Services {
    public class PointOfInterestService {
        private readonly IPointOfInterestRepository _pointOfInterestRepository;
        public PointOfInterestService(IPointOfInterestRepository pointOfInterestRepository) {
            _pointOfInterestRepository = pointOfInterestRepository;
        }
        public List<PointOfInterest> GetAllByTourId(int tourId) {
            return _pointOfInterestRepository.GetAllByTourId(tourId);
        }
        public bool DeleteByTourId(int id) {
            return _pointOfInterestRepository.DeleteMultiple(GetAllByTourId(id));
        }
        public PointOfInterest GetById(int id) {
            return _pointOfInterestRepository.GetById(id);
        }
        public PointOfInterest Save(PointOfInterest pointOfInterest) {
            return _pointOfInterestRepository.Save(pointOfInterest);
        }
        public bool Update(PointOfInterest pointOfInterest) {
            return _pointOfInterestRepository.Update(pointOfInterest);
        }

        public bool DeleteByTours(List<Tour> tours) {
            return _pointOfInterestRepository.DeleteByTours(tours);
        }
    }
}
