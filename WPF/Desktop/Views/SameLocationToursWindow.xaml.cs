using BookingApp.Services;
using BookingApp.WPF.DTO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for SameLocationToursWindow.xaml
    /// </summary>
    public partial class SameLocationToursWindow : Window {
        private TourService _tourService = ServicesPool.GetService<TourService>();
        private TourReservationWindow _parentWindow;
        public TourDTO CurrentTour { get; set; }
        public ObservableCollection<TourDTO> SameLocationTours { get; set; }
        public SameLocationToursWindow(TourDTO currentTour, TourReservationWindow parentWindow) {
            InitializeComponent();
            DataContext = this;
            CurrentTour = currentTour;
            _parentWindow = parentWindow;
            SameLocationTours = new ObservableCollection<TourDTO>();

            Update();
        }

        public void Update() {
            SameLocationTours.Clear();
            foreach (var tour in _tourService.GetSameLocationTours(CurrentTour))
                SameLocationTours.Add(new TourDTO(tour));
        }

        private void ReservationButton_Click(object sender, RoutedEventArgs e) {
            _parentWindow.Close();
            var button = (Button)sender;
            var selectedTour = (TourDTO)button.DataContext;

            if (_tourService.GetAvailableSpace(selectedTour) != 0) {
                TourReservationWindow reservationWindow = new TourReservationWindow(selectedTour, 4);
                reservationWindow.ShowDialog();
            }
        }
    }
}
