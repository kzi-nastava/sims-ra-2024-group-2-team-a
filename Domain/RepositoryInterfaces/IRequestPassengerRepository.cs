using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces {
    public interface IRequestPassengerRepository : IRepository<RequestPassenger> {
        public IEnumerable<RequestPassenger> GetByRequestId(int id);
    }
}
