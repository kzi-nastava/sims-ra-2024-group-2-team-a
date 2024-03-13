using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.AndroidViews
{
    /// <summary>
    /// Interaction logic for SmallNotificationPage.xaml
    /// </summary>
    public partial class SmallNotificationPage : Page
    {
        private int _messages;

        public Frame smallNotificationFrame;
        public SmallNotificationPage(Frame smallNotificationFrame,int messagesNum)
        {
            InitializeComponent();
            this.smallNotificationFrame = smallNotificationFrame;
            this._messages = messagesNum;
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("You have "+ _messages.ToString() + " ungraded reservations!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            smallNotificationFrame.Content = null;
        }
    }
}
