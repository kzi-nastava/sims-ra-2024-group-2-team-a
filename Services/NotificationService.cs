﻿using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;

namespace BookingApp.Services {
    public class NotificationService {
        private readonly INotificationRepository _notificationRepository = RepositoryInjector.GetInstance<INotificationRepository>();
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
            RescheduleRequestService rescheduleRequestService = ServicesPool.GetService<RescheduleRequestService>();
            AccommodationReservationService accResService = ServicesPool.GetService<AccommodationReservationService>();

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

        public void SendTouristNotification(NotificationCategory category, int touristId, int tourId) {
            if(touristId != -1)
                this.Save(new Notification(category, touristId, tourId));
        }
    }
}
