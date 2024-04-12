using BookingApp.DTO;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class TouristReservationsPageViewModel {
        private readonly TourReservationService _tourReservationService = new TourReservationService();
        private readonly TourService _tourService = new TourService();
        public int UserId { get; set; }
        public ObservableCollection<TourDTO> ReservedTours { get; set; }
        public TouristReservationsPageViewModel(int userId) { 
            UserId = userId;
            ReservedTours = new ObservableCollection<TourDTO>();
            Update();
        }

        public void Update() {
            ReservedTours.Clear();
            foreach (var tour in _tourReservationService.GetReservedTours(UserId)) {
                ReservedTours.Add(new TourDTO(tour));
            }
        }

        public bool IsRatingAvailable(TourDTO tour) {
            return _tourService.WasTouristPresent(UserId, tour) && !_tourService.IsTourReviewedForUser(UserId, tour);
        }
    }
}
