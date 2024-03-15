using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.AndroidViews {
    /// <summary>
    /// Interaction logic for AccomodationPage.xaml
    /// </summary>
    public partial class AccommodationPage : Page {
        public Frame mainFrame;

        private readonly User _user;

        private readonly AccommodationRepository _accommodationRepository;

        private readonly LocationRepository _locationRepository;

        public ObservableCollection<AccommodationDTO> AccommodationDTOs { get; set; }

        public AccommodationPage(Frame mFrame, User user) {
            InitializeComponent();
            mainFrame = mFrame;
            this._user = user;
            DataContext = this;
            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            AccommodationDTOs = new ObservableCollection<AccommodationDTO>();

            Update();
        }

        public void Update() {
            foreach (var acc in _accommodationRepository.GetByOwnerId(_user.Id)) {
                AccommodationDTO accDTO = new AccommodationDTO(acc);
                Location location = _locationRepository.GetById(acc.LocationId);
                accDTO.SetDisplayLocation(location.City, location.Country);
                AccommodationDTOs.Add(accDTO);
            }
        }

        private void AddAccomodation_Click(object sender, RoutedEventArgs e) {
            mainFrame.Content = new AddAccommodationPage(mainFrame, _user);
        }
    }
}
