using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository {
    public class VisitedTourRepository : Repository<VisitedTour>, IVisitedTourRepository {
        public IEnumerable<VisitedTour> GetByTouristId(int id) {
            return GetAll().Where(t => t.TouristId == id);
        }

        public void DeleteAllByTouristId(int id) {
            foreach (var instance in GetByTouristId(id))
                Delete(instance);
        }
    }
}
