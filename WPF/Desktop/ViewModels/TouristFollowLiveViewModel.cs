using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class TouristFollowLiveViewModel {
        public TourDTO SelectedTour { get; set; }
        public int UserId { get; set; }
        public ObservableCollection<PointOfInterestDTO> PointsOfInterest { get; set; }
        public TouristFollowLiveViewModel(TourDTO selectedTour, int userId) {
            SelectedTour = selectedTour;
            UserId = userId;
            Update();
        }

        public void Update() {
            PointsOfInterest.Clear();
        }
    }
}
