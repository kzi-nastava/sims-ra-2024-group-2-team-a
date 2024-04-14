using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.RepositoryInterfaces {
    public interface IPointOfInterestRepository : IRepository<PointOfInterest>{
        public List<PointOfInterest> GetAllByTourId(int tourId);
        public bool DeleteMultiple(List<PointOfInterest> points);

    }
}
