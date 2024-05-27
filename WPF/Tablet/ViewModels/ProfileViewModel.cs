using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Tablet.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged{
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _guideId;
        private readonly TourService _tourService = ServicesPool.GetService<TourService>();
        private readonly VoucherService _voucherService = ServicesPool.GetService<VoucherService>();
        private readonly PointOfInterestService _pointOfInterestService = ServicesPool.GetService<PointOfInterestService>();
        private readonly PassengerService _passengerService = ServicesPool.GetService<PassengerService>();
        private readonly TourReservationService _tourReservationService = ServicesPool.GetService<TourReservationService>();
        private readonly GuideService _guideService = ServicesPool.GetService<GuideService>();
        private readonly UserService _userSerService = ServicesPool.GetService<UserService>();
        public GuideProfileDTO guideProfileDTO { get; set; }
        public ProfileViewModel() {

        }
        public ProfileViewModel(int guideId) {
            _guideId = guideId;
            guideProfileDTO = new GuideProfileDTO(_guideService.GetById(_guideId));
        }

        public void Quit() {
            List<Tour> tours = _tourService.GetScheduled(_guideId);
            DeleteTours(tours);
            DeleteReservations(tours);

            _guideService.Delete(_guideId);
            _userSerService.Delete(_guideId);
        }
        public void DeleteReservations(List<Tour> tours) {
            List<TourReservation> reservations = _tourReservationService.GetByTours(tours);
            if (reservations == null)
                return;
            else if (reservations.Count > 0) {
                if (!_passengerService.DeleteByReservations(reservations))
                    return;

                if (!_voucherService.AddMultiple(reservations.Select(x => x.TouristId).Distinct().ToList(), DateTime.Today.AddYears(2)))
                    return;
            }
        }
        public void DeleteTours(List<Tour> tours) {
            if (!_pointOfInterestService.DeleteByTours(tours))
                return;

            if (!_tourService.DeleteMultiple(tours))
                return;

        }
    }
}
