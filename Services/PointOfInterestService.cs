using BookingApp.Model;
using BookingApp.RepositoryInterfaces;
using System.Collections.Generic;

namespace BookingApp.Services {
    public class PointOfInterestService {
        private readonly IPointOfInterestRepository _pointOfInterestRepository = RepositoryInjector.GetInstance<IPointOfInterestRepository>();
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
    }
}
