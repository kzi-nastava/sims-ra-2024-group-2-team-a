using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Desktop.ViewModels {
    public class TourRequestInformationWindowViewModel {
        public TourRequestDTO TourRequest { get; set; }
        public TourRequestInformationWindowViewModel(TourRequestDTO tourRequest) { 
            TourRequest = tourRequest;
        }
    }
}
