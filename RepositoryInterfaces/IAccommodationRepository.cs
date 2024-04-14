using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.RepositoryInterfaces {
    public interface IAccommodationRepository : IRepository<Accommodation> {
        public List<Accommodation> GetByOwnerId(int ownerId);
    }
}
