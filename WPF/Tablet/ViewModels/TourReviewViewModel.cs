using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System.Collections.ObjectModel;

namespace BookingApp.WPF.Tablet.ViewModels {
    public class TourReviewViewModel {
        private readonly PointOfInterestService _pointOfInterestService = ServicesPool.GetService<PointOfInterestService>();
        private readonly TourReviewService _tourReviewService = ServicesPool.GetService<TourReviewService>();
        private readonly TourReservationService _tourReservationService = ServicesPool.GetService<TourReservationService>();
        private readonly PassengerService _passengerService = ServicesPool.GetService<PassengerService>();
        public ObservableCollection<TourReviewDTO> tourReviewDTOs { get; set; }
        public TourDTO tourDTO { get; set; }
        public TourReviewDTO tourReviewDTO { get; set; }

        public TourReviewViewModel(TourDTO tDTO) {
            tourDTO = tDTO;
            tourReviewDTOs = new ObservableCollection<TourReviewDTO>();

            Load();
            
        }
        public TourReviewViewModel(TourReviewDTO tReviewDTO){
            tourReviewDTO = tReviewDTO;
        }
        private void Load() {

            foreach (var review in _tourReviewService.GetByTourId(tourDTO.Id)) {
                TourReviewDTO tempDTO = new TourReviewDTO(review);
                TourReservation tourReservation = _tourReservationService.GetByTourAndTourist(tempDTO.TourId, tempDTO.TouristId);
                PassengerDTO passengerDTO = new PassengerDTO(_passengerService.GetByReservationAndTourist(tourReservation.Id, tempDTO.TouristId));
                PointOfInterest point = _pointOfInterestService.GetById(passengerDTO.JoinedPointOfInterestId);
                if (point != null) {
                    PointOfInterestDTO pointOfInterestDTO = new PointOfInterestDTO(point);
                    passengerDTO.SetPointOfInterestName(pointOfInterestDTO.Name);
                    tempDTO.PointOfInterestName = passengerDTO.JoinedPointOfInterestName;
                }
                else {
                    tempDTO.PointOfInterestName = "(Didn't Come.)";
                }

                tempDTO.TouristName = passengerDTO.Name;
                tempDTO.TouristSurname = passengerDTO.Surname;
                tourReviewDTOs.Add(tempDTO);
            }
        }
        public void Report() {
            tourReviewDTO.IsValid = false;
            _tourReviewService.Update(tourReviewDTO.ToModel());
        }
    }
}
