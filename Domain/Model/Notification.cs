﻿using BookingApp.Serializer;
using System;

namespace BookingApp.Domain.Model {

    public enum NotificationCategory { Review, Request, SuperOwner, Forum, TourActive, TourRequest }
    public class Notification : ISerializable, IIdentifiable
    {

        public int Id { get; set; }
        public string Message { get; set; }
        public NotificationCategory Category { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }

        public bool IsRead { get; set; }
        public int TourId { get; set; }
        public Notification() { }

        public Notification(string message, NotificationCategory category, int userId, DateTime creationDate, bool isRead)
        {
            Message = message;
            Category = category;
            UserId = userId;
            CreationDate = creationDate;
            IsRead = isRead;
            TourId = -1;
        }

        public Notification(NotificationCategory category, int userId, int tourId)
        {
            switch (category) {
                case NotificationCategory.TourActive:
                    Message = "You have been added to one of your active tours.";
                    break;
                case NotificationCategory.TourRequest:
                    Message = "A tour based on your requests has been added.";
                    break;
            }       
            Category = category;
            UserId = userId;
            CreationDate = DateTime.Now;
            IsRead = false;
            TourId = tourId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), UserId.ToString(), Category.ToString(), CreationDate.ToString("dd-MM-yyyy HH:mm:ss"), IsRead.ToString(), Message, TourId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            Enum.TryParse(values[2], out NotificationCategory tmp);
            Category = tmp;
            CreationDate = DateTime.ParseExact(values[3], "dd-MM-yyyy HH:mm:ss", null);
            IsRead = bool.Parse(values[4]);
            Message = values[5];
            TourId = Convert.ToInt32(values[6]);
        }
    }
}
