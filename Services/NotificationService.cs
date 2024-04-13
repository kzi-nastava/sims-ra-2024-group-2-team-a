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

        public void CreateNotifications(int ownerId) {
            RescheduleRequestService rescheduleRequestService = new RescheduleRequestService();
            AccommodationReservationService accResService = new AccommodationReservationService();

            int ungradedReservations = accResService.CheckForNotGradedReservations(ownerId);
            int pendingRescheduleRequests = rescheduleRequestService.GetPendingRequestsByOwnerId(ownerId).Count;

            if (ungradedReservations != 0) {
                string message = $"You have {ungradedReservations} ungraded reservations. Navigate to reservations tab to grade them!";
                Notification notification = new Notification(message, NotificationCategory.Review, ownerId, DateTime.Now, false);
                this.Save(notification);
            }
            if (pendingRescheduleRequests > 0) {
                string message = $"You have {pendingRescheduleRequests} pending reschedule requests. Navigate to reservations/requests tab to accept/decline them!";
                Notification notification = new Notification(message, NotificationCategory.Request, ownerId, DateTime.Now, false);
                this.Save(notification);
            }
        }

        public void CreateSuperNotification(int userId) {
            string message = $"CONGRATULATIONS!! You have just become SUPER owner!!";
            Notification notification = new Notification(message, NotificationCategory.SuperOwner, userId, DateTime.Now, false);
            this.Save(notification);
        }
    }
}
