using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository {
    public class RequestPassengerRepository : Repository<RequestPassenger>, IRequestPassengerRepository {
        public IEnumerable<RequestPassenger> GetByRequestId(int id) {
            return GetAll().Where(p => p.TourRequestId == id);
        }
    }
}
