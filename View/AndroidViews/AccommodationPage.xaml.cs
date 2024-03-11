using BookingApp.DTO;
using BookingApp.Repository;
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
using BookingApp.Model;
using System.Collections.ObjectModel;

namespace BookingApp.View.AndroidViews
{
    /// <summary>
    /// Interaction logic for AccomodationPage.xaml
    /// </summary>
    public partial class AccommodationPage : Page
    {
        public Frame mainFrame;

        private readonly User _user;

        private readonly AccommodationRepository _accommodationRepository;

        private readonly LocationRepository _locationRepository;

        public ObservableCollection<AccommodationDTO> accommodationDTOs { get; set; }
        
        public AccommodationPage(Frame mFrame, User user)
        {
            InitializeComponent();
            mainFrame = mFrame;
            this._user = user;
            DataContext = this;
            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            accommodationDTOs = new ObservableCollection<AccommodationDTO>();

            Update();
        }

        public void Update()
        {
            foreach (var acc in _accommodationRepository.GetByOwnerId(_user.Id))
            {
                AccommodationDTO accDTO = new AccommodationDTO(acc);
                Location location = _locationRepository.GetById(acc.LocationId);
                accDTO.SetDisplayLocation(location.City,location.Country);
                accommodationDTOs.Add(accDTO);
            }
        }

        private void AddAccomodation_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new AddAccommodationPage(mainFrame,_user);
        }
    }
}
