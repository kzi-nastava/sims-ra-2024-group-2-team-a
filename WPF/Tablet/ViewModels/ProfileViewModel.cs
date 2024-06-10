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
        private readonly LanguageService _languageService = ServicesPool.GetService<LanguageService>();
        private readonly TourReviewService _tourReviewService = ServicesPool.GetService<TourReviewService>();
        public GuideProfileDTO guideProfileDTO { get; set; }


        private string _help = "Enable Help";
        public string Help {
            get {
                return _help;
            }
            set {
                if (_help != value) {
                    _help = value;
                    OnPropertyChanged();
                }
            }
        }
        public ProfileViewModel(int guideId) {
            _guideId = guideId;
            guideProfileDTO = new GuideProfileDTO(_guideService.GetById(_guideId));
        }
        public void Update() {
            if (guideProfileDTO.IsSuper && guideProfileDTO.SuperUntil > DateTime.Now)
                return;


            double maxScore = 0;
            foreach (var lan in _languageService.GetAll()) {
                List<Tour> tours = _tourService.GetFinishedByLanguage(_guideId, lan);
                if (tours.Count < 20)
                    continue;

                double avgScore = _tourReviewService.GetAvrageScore(tours);

                if (maxScore < avgScore)
                    maxScore = avgScore;

                if (avgScore > 4.0 && maxScore <= avgScore) {
                    guideProfileDTO.LanguageId = lan.Id;
                    guideProfileDTO.IsSuper = true;
                    guideProfileDTO.Score = maxScore;
                    guideProfileDTO.SuperUntil = DateTime.Now.AddYears(1);
                    _guideService.Update(guideProfileDTO.ToModel());
                }
            }
            if (maxScore <= 4.0) {
                guideProfileDTO.LanguageId = -1;
                guideProfileDTO.IsSuper = false;
                guideProfileDTO.Score = maxScore;
                _guideService.Update(guideProfileDTO.ToModel());
            }
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

        public void HelpPressed() {
            if(guideProfileDTO.IsHelpActive) {
                guideProfileDTO.IsHelpActive = false;
                Help = "Enable Help";
            }
            else {
                guideProfileDTO.IsHelpActive = true;
                Help = "Disable Help";
            }
        }
    }
}
