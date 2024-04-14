using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Tablet.ViewModels {
    public class MenuBarViewModel {
        private int _userId;
        private readonly TourService _tourService = new TourService();
        public TourDTO tourDTO { get; set; }

        public MenuBarViewModel(int userId) {
            _userId = userId;
        }
        public bool IsTourActive() {
            Tour tour = _tourService.GetActive(_userId);
            if (tour == null) 
                return false;

            tourDTO = new TourDTO(tour);
            return true;
        }
    }
}
