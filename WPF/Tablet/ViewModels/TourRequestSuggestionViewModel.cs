using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Tablet.ViewModels
{
    public class TourRequestSuggestionViewModel{

        private readonly TourRequestService _tourRequestService = ServicesPool.GetService<TourRequestService>();
        public TourRequestSuggestionViewModel() {
        }
        public TourDTO MostWantedLocation() {
            TourDTO tDTO = new TourDTO();
            tDTO.LocationId = _tourRequestService.GetMostWantedLocation();
            return tDTO;
        }
        public TourDTO MostWantedLanguage() {
            TourDTO tDTO = new TourDTO();
            tDTO.LocationId = _tourRequestService.GetMostWantedLanguage();
            return tDTO;
        }
    }
}
