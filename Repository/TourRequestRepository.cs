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

        public List<TourRequest> GetByTouristIdForYear(int id, int year) {
            return GetByTouristId(id).Where(a => a.EndDate.Year == year).ToList();
        }

        public List<TourRequest> GetOnHold(int userId) {
            return GetByTouristId(userId).Where(r => r.Status == TourRequestStatus.OnHold).ToList();
        }

        public List<TourRequest> GetExpired(int userId) {
            return GetByTouristId(userId).Where(r => r.Status == TourRequestStatus.Expired).ToList();
        }  

        public List<TourRequest> GetAccepted(int userId) {
            return GetByTouristId(userId).Where(r => r.Status == TourRequestStatus.Accepted).ToList();
        }

        public List<TourRequest> GetAcceptedForYear(int userId, int year) {
            return GetByTouristIdForYear(userId, year).Where(r => r.Status == TourRequestStatus.Accepted).ToList();
        }

        public int GetRequestNumberByLocation(Location location, int userId) {
            return GetByTouristId(userId).Where(r => r.LocationId == location.Id).Count();
        }

        public int GetRequestNumberByLanguage(Language language, int userId) {
            return GetByTouristId(userId).Where(r => r.LanguageId == language.Id).Count();
        }
    }
}
