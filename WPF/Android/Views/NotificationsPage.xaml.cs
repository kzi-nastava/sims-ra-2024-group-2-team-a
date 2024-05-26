using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for NotificationsPage.xaml
    /// </summary>
    public partial class NotificationsPage : Page {

        private NotificationService notificationService = ServicesPool.GetService<NotificationService>();

        private readonly User _user;
        public List<NotificationDTO> NotificationDTOs { get; set; }
        public NotificationsPage(User user) {
            InitializeComponent();
            DataContext = this;

            _user = user;
            NotificationDTOs = new List<NotificationDTO>();
            Update();
        }

        public void Update() {
            foreach (Notification notification in notificationService.GetByUserId(_user.Id)) {
                if (!notification.IsRead) 
                    NotificationDTOs.Add(new NotificationDTO(notification));
            }

            NotificationDTOs = NotificationDTOs.OrderByDescending(notification => notification.CreationDate).ToList();
            NumOfMessagesLabel.Content = "(" + NotificationDTOs.Count + ")";
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null) {
                NotificationDTO item = checkBox.DataContext as NotificationDTO;
                if (item != null) {
                    item.IsRead = true;
                    notificationService.Update(item.ToNotification());
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e) {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null) {
                NotificationDTO item = checkBox.DataContext as NotificationDTO;
                if (item != null) {
                    item.IsRead = false;
                    notificationService.Update(item.ToNotification());
                }
            }
        }

        private void MarkReadButton_Click(object sender, RoutedEventArgs e) {
            foreach (var notificationDTO in NotificationDTOs) {
                notificationDTO.IsRead = true;
                notificationService.Update(notificationDTO.ToNotification());
            }
        }
    }
}
