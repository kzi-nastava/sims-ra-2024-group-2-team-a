using BookingApp.Services;
using BookingApp.WPF.Android.ViewModels;
using BookingApp.WPF.DTO;
using BookingApp.WPF.Tablet.Views;
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

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for AccommodationRenovationPage.xaml
    /// </summary>
    public partial class AccommodationRenovationPage : Page {
        private readonly Frame _mainFrame;
        public AccommodationRenovationViewmodel AccommodationRenovationViewmodel { get; set; }
        public AccommodationRenovationPage(AccommodationDTO accommodationDTO, Frame mainFrame) {
            InitializeComponent();

            AccommodationRenovationViewmodel = new AccommodationRenovationViewmodel(accommodationDTO);
            _mainFrame = mainFrame;

            DataContext = AccommodationRenovationViewmodel;
            startDatePicker.DisplayDateStart = DateTime.Now;
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e) {

        }

        private void DeclineButton_Click(object sender, RoutedEventArgs e) {
            _mainFrame.GoBack();
        }

        private void startDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {

        }
    }
}
