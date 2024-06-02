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
        public List<TourRequest> GetByTouristIdForYear(int id, int year);
        public List<TourRequest> GetOnHold(int userId);
        public List<TourRequest> GetExpired(int userId);
        public List<TourRequest> GetAccepted(int userId);
        public List<TourRequest> GetNotAccepted(int userId);
        public List<TourRequest> GetAcceptedForYear(int userId, int year);
        public int GetRequestNumberByLocation(Location location, int userId);
        public int GetRequestNumberByLanguage(Language language, int userId);
    }
}
