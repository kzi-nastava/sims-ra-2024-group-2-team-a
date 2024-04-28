using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Web.ViewModels {
    public class CreateReservationPageViewModel : INotifyPropertyChanged {

        public AccommodationDTO Accommodation { get; set; }
        public AccommodationReservationDTO ReservationDTO { get; set; } = new AccommodationReservationDTO();

        private List<AccommodationReservation> _suggestedReservations;
        public List<AccommodationReservation> SuggestedReservations {
            get { return _suggestedReservations; }
            set {
                _suggestedReservations = value;
                OnPropertyChanged(nameof(SuggestedReservations));
            }
        }

        private readonly int maxSuggestedReservationsCount = 20;

        private readonly AccommodationReservationService _reservationService = new AccommodationReservationService();

        private readonly GuestService _guestService = new GuestService();

        public Guest GuestUser { get; set; }
        public int MaxBonusPoints { get; set; } = Guest.SuperGuestStartPoints;

        public CreateReservationPageViewModel(AccommodationDTO accommodationDTO, int guestId) {
            Accommodation = accommodationDTO;
            ReservationDTO.AccommodationId = Accommodation.Id;

            GuestUser = _guestService.GetById(guestId);
        }

        public void UpdateSuggestedReservations() {
            SuggestedReservations = _reservationService.SuggestReservations(ReservationDTO);

            if (SuggestedReservations.Count > maxSuggestedReservationsCount)
                SuggestedReservations = SuggestedReservations.GetRange(0, maxSuggestedReservationsCount);

            SuggestedReservations = SuggestedReservations.OrderBy(r => r.StartDate).ToList();
        }

        public void SaveReservation(AccommodationReservation selectedReservation) {
            selectedReservation.GuestId = GuestUser.Id;
            _reservationService.Save(selectedReservation);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
