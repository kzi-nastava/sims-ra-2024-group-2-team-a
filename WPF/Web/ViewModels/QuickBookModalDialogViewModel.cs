using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.Web.ViewModels {
    public class QuickBookModalDialogViewModel {

        public AccommodationDTO Accommodation { get; set; }
        public List<AccommodationReservation> SuggestedReservations { get; set; }
        public AccommodationReservationFilterDTO ReservationFilter { get; set; }
        public Guest GuestUser { get; set; }
        public int MaxBonusPoints { get; set; } = Guest.SuperGuestStartPoints;

        public AccommodationReservation SelectedReservation { get; set; }

        public ICommand CreateReservation { get; set; }

        private readonly GuestService _guestService = ServicesPool.GetService<GuestService>();
        private readonly AccommodationReservationService _reservationService = ServicesPool.GetService<AccommodationReservationService>();

        public QuickBookModalDialogViewModel(AccommodationDTO accommodation, int guestId, AccommodationReservationFilterDTO resFilter) {
            Accommodation = accommodation;
            ReservationFilter = resFilter;
            GuestUser = _guestService.GetById(guestId);

            CheckDates();

            SuggestedReservations = _reservationService.SuggestReservations(resFilter);

            CreateReservation = new RelayCommand(SaveReservation, () => {
                return SelectedReservation != null;
            });
        }

        private void CheckDates() {
            DateOnly emptyDate = new DateOnly();

            if(ReservationFilter.StartDate == emptyDate) {
                ReservationFilter.StartDate = DateOnly.FromDateTime(DateTime.Now);
            }

            if(ReservationFilter.EndDate == emptyDate) {
                ReservationFilter.EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(20));
            }
        }

        public void SaveReservation(object parameter) {
            SelectedReservation.GuestsNumber = ReservationFilter.GuestsNumber;
            SelectedReservation.GuestId = GuestUser.Id;
            _reservationService.Save(SelectedReservation);
        }
    }
}
