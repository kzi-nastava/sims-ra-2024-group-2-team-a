using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;

namespace BookingApp.Services {
    public class NotificationService {
        private readonly INotificationRepository _notificationRepository;

        private AccommodationRescheduleRequestService _rescheduleRequestService;
        private AccommodationReservationService _reservationService;
        private LocationService _locationService;
        private AccommodationService _accommodationService;

        public NotificationService(INotificationRepository notificationRepository) {
            _notificationRepository = notificationRepository;
        }

        public void InjectServices(AccommodationRescheduleRequestService rescheduleRequestService, 
                AccommodationReservationService reservationService, 
                LocationService locationService, AccommodationService accService) {
            _rescheduleRequestService = rescheduleRequestService;
            _reservationService = reservationService;
            _locationService = locationService;
            _accommodationService = accService;
        }

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
            int ungradedReservations = _reservationService.CheckForNotGradedReservations(ownerId);
            int pendingRescheduleRequests = _rescheduleRequestService.GetPendingRequestsByOwnerId(ownerId).Count;

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

        public void SendTouristNotification(NotificationCategory category, int touristId, int tourId) {
            if(touristId != -1)
                this.Save(new Notification(category, touristId, tourId));
        }

        //pozvati ovu metodu kad se napravi novi forum
        public void CreateNewForumNotification(int locationId) {
            Location loc = _locationService.GetById(locationId);
            List<int> ownerIds = new List<int>();

            foreach (var acc in _accommodationService.GetByLocationId(locationId)) {
                if (!ownerIds.Contains(acc.OwnerId)) {
                    ownerIds.Add(acc.OwnerId);
                }
            }

            foreach (var ownerId in ownerIds) {
                string message = $"New forum opened in {loc.Country}, {loc.City}!";
                Notification notification = new Notification(message, NotificationCategory.Forum, ownerId, DateTime.Now, false);
                this.Save(notification);
            }
        }

        public void UpdateNotificationStatus(int notificationId) {
            Notification notification = _notificationRepository.GetById(notificationId);
            notification.IsRead = true;
            _notificationRepository.Update(notification);
        }
    }
}
