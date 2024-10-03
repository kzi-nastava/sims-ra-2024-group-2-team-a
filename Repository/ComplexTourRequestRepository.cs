using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository {
    public class ComplexTourRequestRepository : Repository<ComplexTourRequest>, IComplexTourRequestRepository{
        public IEnumerable<ComplexTourRequest> GetByTouristId(int touristId) {
            return GetAll().Where(r => r.TouristId == touristId);
        }

        public IEnumerable<ComplexTourRequest> GetAccepted(int touristId) {
            return GetByTouristId(touristId).Where(r => r.Status == TourRequestStatus.Accepted);
        }

        public IEnumerable<ComplexTourRequest> GetOnHold(int touristId) {
            return GetByTouristId(touristId).Where(r => r.Status == TourRequestStatus.OnHold);
        }

        public IEnumerable<ComplexTourRequest> GetExpired(int touristId) {
            return GetByTouristId(touristId).Where(r => r.Status == TourRequestStatus.Expired);
        }
    }
}
