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
using System.Windows.Shapes;

namespace BookingApp.View.WebViews {
    /// <summary>
    /// Interaction logic for GuestMainWindow.xaml
    /// </summary>
    public partial class GuestMainWindow : Window {
        public GuestMainWindow() {
            InitializeComponent();
            SetWindowProperties();
            SetActiveUserControl(bookPage);
        }

        private void SetWindowProperties() {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void SetActiveUserControl(UserControl userControl) {
            bookPage.Visibility = Visibility.Collapsed;
            reservationsPage.Visibility = Visibility.Collapsed;

            userControl.Visibility = Visibility.Visible;
        }

        private void buttonBook_Click(object sender, RoutedEventArgs e) {
            SetActiveUserControl(bookPage);
        }

        private void buttonReservations_Click(object sender, RoutedEventArgs e) {
            SetActiveUserControl(reservationsPage);
        }
    }
}
