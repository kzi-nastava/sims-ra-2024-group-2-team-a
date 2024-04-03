using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class NotificationService {
        private readonly NotificationRepository _notificationRepository = new NotificationRepository();
        public NotificationService() { }

        public void Save(Notification notification) {
            _notificationRepository.Save(notification);
        }
        public List<Notification> GetByUserId(int userId) {
            return _notificationRepository.GetAll().FindAll(x => x.UserId == userId);
        }
        public void Update(Notification notification) {
            _notificationRepository.Update(notification);
        }
    }
}
