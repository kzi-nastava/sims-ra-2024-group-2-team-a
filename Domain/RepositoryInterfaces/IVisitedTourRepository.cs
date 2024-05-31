using BookingApp.Domain.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces {
    public interface IVisitedTourRepository : IRepository<VisitedTour> {
        public IEnumerable<VisitedTour> GetByTouristId(int id);

        public void DeleteAllByTouristId(int id);
    }
}
