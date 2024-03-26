﻿using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO {
    public class NotificationDTO: INotifyPropertyChanged {
        public NotificationDTO() { }

        public NotificationDTO(Notification notification) {
            Id = notification.Id;
            UserId = notification.UserId;
            Message = notification.Message;
            CreationDate = notification.CreationDate;
            Category = notification.Category;
            IsRead = notification.IsRead;
            SetNotificationIcon();
        }
        public int Id { get; set; } 
        public int UserId { get; set; }

        public NotificationCategory Category { get; set; }

        public string _message;
        public string Message {
            get {
                return _message;
            }
            set {
                if (value != _message) {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _creationDate;
        public DateTime CreationDate {
            get {
                return _creationDate;
            }
            set {
                if (value != _creationDate) {
                    _creationDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isRead;
        public bool IsRead {
            get {
                return _isRead;
            }
            set {
                if (value != _isRead) {
                    _isRead = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _notificationIcon;
        public string NotificationIcon {
            get {
                return _notificationIcon;
            }
            set {
                if (value != _notificationIcon) {
                    _notificationIcon = value;
                    OnPropertyChanged();
                }
            }
        }

        public Notification ToNotification() {
            Notification notification = new Notification(Message, Category, UserId, CreationDate, IsRead);
            notification.Id = this.Id;
            return notification;
        }

        public void SetNotificationIcon() {
            NotificationIcon = "../../Resources/Images/";
            if (Category == NotificationCategory.Review) {
                NotificationIcon += "notification-review-icon.png";
            }
            if (Category == NotificationCategory.Request) {
                NotificationIcon += "notification-request-icon.png";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}