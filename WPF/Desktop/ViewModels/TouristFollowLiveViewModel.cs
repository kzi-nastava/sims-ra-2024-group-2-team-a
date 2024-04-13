using BookingApp.DTO;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class TouristFollowLiveViewModel {
        private readonly PointOfInterestService _pointOfInterestService = new PointOfInterestService();
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
            foreach (var point in _pointOfInterestService.GetAllByTourId(SelectedTour.Id)) {
                PointsOfInterest.Add(new PointOfInterestDTO(point));
            }
        }
    }
}
