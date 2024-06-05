using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.ComponentModel;

namespace BookingApp.WPF.Web.ViewModels {

    public class ReviewAccommodationModalDialogViewModel : INotifyPropertyChanged {

        public AccommodationReservationDTO Reservation { get; set; }
        public ImportanceType[] ImportanceTypes { get; set; } = (ImportanceType[])Enum.GetValues(typeof(ImportanceType));
        public AccommodationReviewDTO Review { get; set; } = new AccommodationReviewDTO();

        public string _selectedAccommodationPicture;
        public string SelectedAccommodationPicture {
            get { return _selectedAccommodationPicture; }
            set {
                _selectedAccommodationPicture = value;
                OnPropertyChanged(nameof(SelectedAccommodationPicture));
            }
        }

        public int PicturesIndex { get; set; } = 0;
        public int MaxPictureIndex => Review.AccommodationPhotos.Count - 1;

        private readonly AccommodationReviewService _reviewService = ServicesPool.GetService<AccommodationReviewService>();

        public ReviewAccommodationModalDialogViewModel(AccommodationReservationDTO reservation) {
            Reservation = reservation;

            Review.ReservationId = Reservation.Id;
            Review.GuestId = Reservation.GuestId;
            Review.OwnerId = Reservation.Accommodation.OwnerId;
        }

        public void GradeOwner() {
            _reviewService.GradeOwner(Review);
        }

        public void RemovePicture() {
            Review.AccommodationPhotos.RemoveAt(PicturesIndex);
            PicturesIndex = 0;
        }

        public void ChangePictureRight() {
            PicturesIndex++;
            PicturesIndex = Math.Min(PicturesIndex, Review.AccommodationPhotos.Count - 1);

            SelectedAccommodationPicture = Review.AccommodationPhotos[PicturesIndex];
        }

        public void ChangePictureLeft() {
            PicturesIndex--;
            PicturesIndex = Math.Max(PicturesIndex, 0);

            SelectedAccommodationPicture = Review.AccommodationPhotos[PicturesIndex];
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
