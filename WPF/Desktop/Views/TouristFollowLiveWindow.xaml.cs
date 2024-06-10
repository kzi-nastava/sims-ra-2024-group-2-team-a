using BookingApp.WPF.Desktop.ViewModels;
using BookingApp.WPF.DTO;
using System.Collections.ObjectModel;
using System.Windows;

namespace BookingApp.WPF.Desktop.Views {
    /// <summary>
    /// Interaction logic for TouristFollowLiveWindow.xaml
    /// </summary>
    public partial class TouristFollowLiveWindow : Window {
        public TouristFollowLiveViewModel viewModel { get; set; }
        public TourDTO SelectedTour { get; set; }
        public int UserId { get; set; }

        public ObservableCollection<PointOfInterestDTO> PointsOfInterest { get; set; }
        public TouristFollowLiveWindow(TourDTO selectedTour, int userId) {
            InitializeComponent();

            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            this.Width = screenWidth * 0.7;
            this.Height = screenHeight * 0.7;

            viewModel = new TouristFollowLiveViewModel(selectedTour, userId);
            DataContext = viewModel;
        }
    }
}
