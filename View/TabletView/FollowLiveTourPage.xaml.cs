using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.View.TabletView
{
    /// <summary>
    /// Interaction logic for FollowLiveTourPage.xaml
    /// </summary>
    public partial class FollowLiveTourPage : Page
    {
        private Frame mainFrame;

        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly LanguageRepository _languageRepository;
        public TourDTO tourDTO { get; set; }
        public ObservableCollection<TourDTO> tourDTOs { get; set; }
        public FollowLiveTourPage(Frame mainF)
        {
            InitializeComponent();
            DataContext = this;

            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _languageRepository = new LanguageRepository();

            tourDTOs = new ObservableCollection<TourDTO>();
            LoadLiveTours();
            
        }
        private void LoadLiveTours() {
            foreach (var tour in _tourRepository.GetToursToday()) {
                TourDTO tempTourDTO = new TourDTO(tour);
                Location location = _locationRepository.GetById(tour.LocationId);
                string city = location.City;
                string country = location.Country;
                tempTourDTO.setLocationTemplate(city, country);
                tempTourDTO.LanguageTemplate = _languageRepository.GetById(tour.LanguageId).Name;
                tourDTOs.Add(tempTourDTO);
            }
        }
    }
}
