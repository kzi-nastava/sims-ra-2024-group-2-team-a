using BookingApp.DTO;
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

namespace BookingApp.View.WebViews {
    /// <summary>
    /// Interaction logic for BookPage.xaml
    /// </summary>
    public partial class BookPage : UserControl {

        private ObservableCollection<AccommodationDTO> _accommodationDTOs = new ObservableCollection<AccommodationDTO>();
        private AccommodationRepository _accommodationRepository = new AccommodationRepository();

        public BookPage() {

            InitializeComponent();

            Update();

            itemsControlAccommodations.ItemsSource = _accommodationDTOs;
        }

        public void Update() {
            var accommodations = _accommodationRepository.GetAll();

            _accommodationDTOs = new ObservableCollection<AccommodationDTO>(
                accommodations.Select(a => new AccommodationDTO(a))
                );
        }
    }
}
