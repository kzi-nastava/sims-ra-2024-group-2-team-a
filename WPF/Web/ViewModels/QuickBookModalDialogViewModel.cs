using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Web.ViewModels {
    public class QuickBookModalDialogViewModel {

        public AccommodationDTO Accommodation { get; set; }
        public List<AccommodationReservation> SuggestedReservations { get; set; }
        public AccommodationReservationFilterDTO ReservationFilter { get; set; }
        public Guest GuestUser { get; set; }
        public int MaxBonusPoints { get; set; } = Guest.SuperGuestStartPoints;

        private readonly GuestService _guestService = ServicesPool.GetService<GuestService>();
        private readonly AccommodationReservationService _reservationService = ServicesPool.GetService<AccommodationReservationService>();

        public QuickBookModalDialogViewModel(AccommodationDTO accommodation, int guestId, AccommodationReservationFilterDTO resFilter) {
            Accommodation = accommodation;
            ReservationFilter = resFilter;
            GuestUser = _guestService.GetById(guestId);

            SuggestedReservations = _reservationService.SuggestReservations(resFilter);
        }

        public void SaveReservation(AccommodationReservation selectedReservation) {
            selectedReservation.GuestsNumber = ReservationFilter.GuestsNumber;
            selectedReservation.GuestId = GuestUser.Id;
            _reservationService.Save(selectedReservation);
        }
    }
}
