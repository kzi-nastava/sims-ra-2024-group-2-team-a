using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces {
    public interface IComplexTourRequestRepository : IRepository<ComplexTourRequest> {
        public IEnumerable<ComplexTourRequest> GetByTouristId(int touristId);
        public IEnumerable<ComplexTourRequest> GetAccepted(int touristId);
        public IEnumerable<ComplexTourRequest> GetOnHold(int touristId);
        public IEnumerable<ComplexTourRequest> GetExpired(int touristId);
    }
}
