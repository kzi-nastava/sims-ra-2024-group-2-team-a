﻿using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.Desktop.Views;
using BookingApp.WPF.DTO;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class TouristNotificationsPageViewModel {
        private readonly NotificationService _notificationService = ServicesPool.GetService<NotificationService>();
        private readonly TourService _tourService = ServicesPool.GetService<TourService>();
        public ObservableCollection<NotificationDTO> Notifications { get; set; }
        public int UserId { get; set; }
        public ICommand NotificationCommand { get; set; }
        public TouristNotificationsPageViewModel(int userId) {
            UserId = userId;
            Notifications = new ObservableCollection<NotificationDTO>();
            Update();
            NotificationCommand = new RelayCommand(NotificationClick, NotificationCanExecute);
        }

        public void Update() {
            Notifications.Clear();
            foreach (var notification in _notificationService.GetByUserId(UserId)) {
                Notifications.Add(new NotificationDTO(notification));
            }
        }

        public void NotificationClick(object parameter) {
            NotificationDTO notification = (NotificationDTO)parameter;
            TourDTO notifiedTour = new TourDTO(_tourService.GetById(notification.TourId));

            if (notification != null) {
                switch (notification.Category) {
                    case NotificationCategory.TourActive:
                        TouristFollowLiveWindow window = new TouristFollowLiveWindow(notifiedTour, UserId);
                        window.ShowDialog();
                        break;
                    case NotificationCategory.TourRequest:
                        TourReservationWindow reservationWindow = new TourReservationWindow(notifiedTour, UserId);
                        reservationWindow.ShowDialog();
                        break;
                    default:
                        break;
                }
            }
        }

        private bool NotificationCanExecute() {
            return true;
        }
    }
}