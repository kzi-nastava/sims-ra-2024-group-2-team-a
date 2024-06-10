using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {
    public class NotificationRepository: Repository<Notification>, INotificationRepository {
        public List<Notification> GetReadForUser(int userId) {
            return GetAll().Where(n => n.UserId == userId && n.IsRead == true).ToList();
        }
        public List<Notification> GetNonReadForUser(int userId) {
            return GetAll().Where(n => n.UserId == userId && n.IsRead == false).ToList();
        }
    }
}
