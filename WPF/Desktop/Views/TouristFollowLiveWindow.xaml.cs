using BookingApp.DTO;
using BookingApp.WPF.Desktop.ViewModels;
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
using System.Windows.Shapes;

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
            viewModel = new TouristFollowLiveViewModel(selectedTour, userId);
            DataContext = viewModel;
        }

        public void Update() {
            PointsOfInterest.Clear();
        }
    }
}
