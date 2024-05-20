using BookingApp.Domain.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.DTO {
    public class NotificationDTO : INotifyPropertyChanged
    {
        public NotificationDTO() { }

        public NotificationDTO(Notification notification)
        {
            Id = notification.Id;
            UserId = notification.UserId;
            Message = notification.Message;
            CreationDate = notification.CreationDate;
            Category = notification.Category;
            IsRead = notification.IsRead;
            TourId = notification.TourId;
            SetNotificationIcon();
            SetNotificationTitles();
        }
        public int Id { get; set; }
        public int UserId { get; set; }

        public NotificationCategory Category { get; set; }

        private string _title;
        public string Title {
            get {
                return _title;
            }
            set {
                if(_title != value) {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _messageHeader;
        public string MessageHeader {
            get {
                return _messageHeader;
            }
            set {
                if(_messageHeader != value) {
                    _messageHeader = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (value != _message)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _tourId;
        public int TourId
        {
            get
            {
                return _tourId;
            }
            set
            {
                if (value != _tourId)
                {
                    _tourId = value;
                    OnPropertyChanged();
                }
            }
        }


        private DateTime _creationDate;
        public DateTime CreationDate
        {
            get
            {
                return _creationDate;
            }
            set
            {
                if (value != _creationDate)
                {
                    _creationDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isRead;
        public bool IsRead
        {
            get
            {
                return _isRead;
            }
            set
            {
                if (value != _isRead)
                {
                    _isRead = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _notificationIcon;
        public string NotificationIcon
        {
            get
            {
                return _notificationIcon;
            }
            set
            {
                if (value != _notificationIcon)
                {
                    _notificationIcon = value;
                    OnPropertyChanged();
                }
            }
        }

        public Notification ToNotification()
        {
            Notification notification = new Notification(Message, Category, UserId, CreationDate, IsRead);
            notification.Id = Id;
            return notification;
        }

        public void SetNotificationTitles() {
            switch (Category) { 
                case NotificationCategory.TourActive:
                    Title = "Reservation";
                    MessageHeader = "Welcome to the tour!";
                    break;
                case NotificationCategory.TourRequest:
                    Title = "Request";
                    MessageHeader = "There is a new tour that might interest you!";
                    break;
                default:
                    Title = "";
                    MessageHeader = "";
                    break;
            }
        }

        public void SetNotificationIcon()
        {
            NotificationIcon = "../../../Resources/Images/";
            if (Category == NotificationCategory.Review)
            {
                NotificationIcon += "notification-review-icon.png";
            }
            if (Category == NotificationCategory.Request)
            {
                NotificationIcon += "notification-request-icon.png";
            }
            if (Category == NotificationCategory.SuperOwner)
            {
                NotificationIcon += "notification-super-icon.png";
            }
            if (Category == NotificationCategory.Forum) {
                NotificationIcon += "forums-icon.png";
            }
            if (Category == NotificationCategory.TourActive) {
                NotificationIcon += "Icons/black-book.png";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)

        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
