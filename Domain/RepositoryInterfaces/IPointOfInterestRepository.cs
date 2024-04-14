using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces {
    public interface IPointOfInterestRepository : IRepository<PointOfInterest>
    {
        public List<PointOfInterest> GetAllByTourId(int tourId);
        public bool DeleteMultiple(List<PointOfInterest> points);

    }
}
