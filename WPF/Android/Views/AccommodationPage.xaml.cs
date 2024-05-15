using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
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

        private AccommodationService accommodationService = ServicesPool.GetService<AccommodationService>();

        private readonly AccommodationStatisticsService _statisticsService = ServicesPool.GetService<AccommodationStatisticsService>();  

        private LocationService locationService = ServicesPool.GetService<LocationService>();
        public ObservableCollection<AccommodationDTO> AccommodationDTOs { get; set; }

        public AccommodationDTO SelectedAccommodation { get; set; }

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
            mainFrame.Content = new AddAccommodationPage(mainFrame, _user, null);
        }

        private void RenovationsButton_Click(object sender, RoutedEventArgs e) {
            if (SelectedAccommodation == null) {
                return;
            }
            else {
                mainFrame.Content = new AccommodationRenovationPage(SelectedAccommodation, mainFrame);
            }
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e) {
            if (SelectedAccommodation == null ) {
                return;
            }
            else if (_statisticsService.IsStatisticEmpty(SelectedAccommodation.Id)) {
                AndroidDialogWindow dialogWindow = new AndroidDialogWindow("Selected accommodation does not have any required statistic!");
                dialogWindow.ShowDialog();
            }
            else {
                mainFrame.Content = new AccommodationStatisticsPage(SelectedAccommodation);
            }
        }

        private void GuidanceButton_Click(object sender, RoutedEventArgs e) {
            if (_statisticsService.GetHottestAndColdestLocations(_user.Id).Count == 1) {
                AndroidDialogWindow dialogWindow = new AndroidDialogWindow("Insufficent accommodations registered on various for this feature!");
                dialogWindow.ShowDialog();
                return;
            }

            mainFrame.NavigationService.Navigate(new AccommodationGuidancePage(_user,mainFrame));
        }

        private void GeneratePdfButton_Click(object sender, RoutedEventArgs e) {

        }
    }
}
