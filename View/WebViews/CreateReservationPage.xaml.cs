using BookingApp.DTO;
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

namespace BookingApp.View.WebViews {
    /// <summary>
    /// Interaction logic for CreateReservationPage.xaml
    /// </summary>
    public partial class CreateReservationPage : Page {

        private AccommodationDTO _accommodationDTO;

        public CreateReservationPage(AccommodationDTO accommodationDTO) {
            InitializeComponent();
            _accommodationDTO = accommodationDTO;
            DataContext = _accommodationDTO;
        }

        private void ButtonBackClick(object sender, RoutedEventArgs e) {
            Frame frame = (Frame)Window.GetWindow(this).FindName("mainFrame");
            frame.Content = new BookingPage(frame);
        }
    }
}
