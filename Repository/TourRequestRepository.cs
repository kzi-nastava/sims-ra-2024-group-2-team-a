using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository {
    public class TourRequestRepository : Repository<TourRequest>, ITourRequestRepository {
        public List<TourRequest> GetByTouristId(int id) {
            return GetAll().Where(r => r.TouristId == id).ToList();
        }
    }
}
