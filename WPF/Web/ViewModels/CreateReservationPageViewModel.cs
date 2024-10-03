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
using System.Windows.Input;

namespace BookingApp.WPF.Web.ViewModels {
    public class CreateReservationPageViewModel : INotifyPropertyChanged {

        public AccommodationDTO Accommodation { get; set; }

        public string _selectedAccommodationPicture;
        public string SelectedAccommodationPicture {
            get { return _selectedAccommodationPicture; }
            set {
                _selectedAccommodationPicture = value;
                OnPropertyChanged(nameof(SelectedAccommodationPicture));
            }
        }

        public int PicturesIndex { get; set; } = 0;
        public int MaxPictureIndex => Accommodation.ProfilePictures.Count - 1;

        public AccommodationReservationFilterDTO ReservationFilter { get; set; } = new AccommodationReservationFilterDTO();

        private List<AccommodationReservation> _suggestedReservations;
        public List<AccommodationReservation> SuggestedReservations {
            get { return _suggestedReservations; }
            set {
                _suggestedReservations = value;
                OnPropertyChanged(nameof(SuggestedReservations));
            }
        }

        public AccommodationReservation SelectedReservation { get; set; }

        private readonly int maxSuggestedReservationsCount = 20;

        public Guest GuestUser { get; set; }
        public int MaxBonusPoints { get; set; } = Guest.SuperGuestStartPoints;

        public double AverageCleannessGrade { get; set; } = 0.0;
        public double AverageCorrectnessGrade { get; set; } = 0.0;

        private readonly AccommodationReservationService _reservationService = ServicesPool.GetService<AccommodationReservationService>();
        private readonly AccommodationReviewService _reviewService = ServicesPool.GetService<AccommodationReviewService>();
        private readonly GuestService _guestService = ServicesPool.GetService<GuestService>();

        public ICommand CreateReservation { get; set; }

        public CreateReservationPageViewModel(AccommodationDTO accommodationDTO, int guestId) {
            Accommodation = accommodationDTO;
            ReservationFilter.AccommodationId = Accommodation.Id;

            GuestUser = _guestService.GetById(guestId);
            SelectedAccommodationPicture = Accommodation.ProfilePictures[PicturesIndex];

            AverageCleannessGrade = _reviewService.GetAverageCleannessGrade(Accommodation.Id);
            AverageCorrectnessGrade = _reviewService.GetAverageCorrectnessGrade(Accommodation.Id);

            CreateReservation = new RelayCommand(SaveReservation, () => {
                return SelectedReservation != null;
            });
        }
        
        public void UpdateSuggestedReservations() {
            SuggestedReservations = _reservationService.SuggestReservations(ReservationFilter);

            if (SuggestedReservations.Count > maxSuggestedReservationsCount)
                SuggestedReservations = SuggestedReservations.GetRange(0, maxSuggestedReservationsCount);

            SuggestedReservations = SuggestedReservations.OrderBy(r => r.StartDate).ToList();
        }

        public void SaveReservation(object parameter) {
            SelectedReservation.GuestId = GuestUser.Id;
            _reservationService.Save(SelectedReservation);
        }

        public void ChangePictureRight() {
            PicturesIndex++;
            PicturesIndex = Math.Min(PicturesIndex, Accommodation.ProfilePictures.Count - 1);

            SelectedAccommodationPicture = Accommodation.ProfilePictures[PicturesIndex];
        }

        public void ChangePictureLeft() {
            PicturesIndex--;
            PicturesIndex = Math.Max(PicturesIndex, 0);

            SelectedAccommodationPicture = Accommodation.ProfilePictures[PicturesIndex];
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
