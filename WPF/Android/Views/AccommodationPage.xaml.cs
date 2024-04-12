using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Android.Views {
    /// <summary>
    /// Interaction logic for AccomodationPage.xaml
    /// </summary>
    public partial class AccommodationPage : Page {
        public Frame mainFrame;

        private readonly User _user;

        private AccommodationService accommodationService = new AccommodationService();

        private LocationService locationService = new LocationService();
        public ObservableCollection<AccommodationDTO> AccommodationDTOs { get; set; }

        public AccommodationPage(Frame mFrame, User user) {
            InitializeComponent();
            mainFrame = mFrame;
            this._user = user;
            DataContext = this;
            AccommodationDTOs = new ObservableCollection<AccommodationDTO>();

            Update();
        }

        public void Update() {
            foreach (var acc in accommodationService.GetByOwnerId(_user.Id)) {
                AccommodationDTO accDTO = new AccommodationDTO(acc);
                Location location = locationService.GetById(acc.LocationId);
                accDTO.SetDisplayLocation(location.City, location.Country);
                AccommodationDTOs.Add(accDTO);
            }
        }

        private void AddAccomodation_Click(object sender, RoutedEventArgs e) {
            mainFrame.Content = new AddAccommodationPage(mainFrame, _user);
        }

        private void RenovationsButton_Click(object sender, RoutedEventArgs e) {

        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e) {

        }

        private void GuidanceButton_Click(object sender, RoutedEventArgs e) {

        }
    }
}
