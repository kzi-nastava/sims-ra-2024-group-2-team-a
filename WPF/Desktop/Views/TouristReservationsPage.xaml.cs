using BookingApp.WPF.Desktop.ViewModels;
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

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for TouristReservationsPage.xaml
    /// </summary>
    public partial class TouristReservationsPage : Page {
        public TouristReservationsPageViewModel viewModel { get; set; }
        public TouristReservationsPage(int userId) {
            InitializeComponent();
            viewModel = new TouristReservationsPageViewModel(userId);
            DataContext = viewModel;
        }

        private void RateTourButton_Click(object sender, RoutedEventArgs e) {

        }

        private void FollowLiveButton_Click(object sender, RoutedEventArgs e) {

        }
    }
}
