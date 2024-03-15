using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.AndroidViews {
    /// <summary>
    /// Interaction logic for SmallNotificationPage.xaml
    /// </summary>
    public partial class SmallNotificationPage : Page {
        private int _messages;

        public Frame smallNotificationFrame;
        public SmallNotificationPage(Frame smallNotificationFrame, int messagesNum) {
            InitializeComponent();
            this.smallNotificationFrame = smallNotificationFrame;
            this._messages = messagesNum;
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("You have " + _messages.ToString() + " ungraded reservations!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            smallNotificationFrame.Content = null;
        }
    }
}
