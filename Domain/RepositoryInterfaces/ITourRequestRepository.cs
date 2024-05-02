using BookingApp.Domain.Model;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces {
    public interface ITourRequestRepository : IRepository<TourRequest> {
        public List<TourRequest> GetByTouristId(int touristId);
    }
}
