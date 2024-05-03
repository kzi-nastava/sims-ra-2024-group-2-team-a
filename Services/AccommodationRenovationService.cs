using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class AccommodationRenovationService {

        private readonly IAccommodationRenovationRepository _renovationRepository;

        private readonly AccommodationReservationService _accommodationReservationService;

        public AccommodationRenovationService(IAccommodationRenovationRepository renovationRepository, AccommodationReservationService reservationService) { 
            _renovationRepository = renovationRepository;
            _accommodationReservationService = reservationService;
        }

        public AccommodationRenovation GetById(int id) {
            return _renovationRepository.GetById(id);
        }

        public void Save(AccommodationRenovation accommodationRenovation) {
            _renovationRepository.Save(accommodationRenovation);
        }

        public bool Update(AccommodationRenovation accommodationRenovation) {
            return _renovationRepository.Update(accommodationRenovation);
        }
        public List<AccommodationRenovation> GetAllPendingRenovations() {
            return _renovationRepository.GetAll().Where(x => x.RenovationState == RenovationState.Pending).ToList();
        }

        public List<AccommodationRenovation> GetByAccommodationId(int accomodationId) {
            return _renovationRepository.GetAll().Where(x => x.AccommodationId == accomodationId).ToList();
        }

        public List<AccommodationRenovation> GetPendingByAccommodationId(int accomodationId) {
            return _renovationRepository.GetAll().Where(x => x.AccommodationId == accomodationId && x.RenovationState == RenovationState.Pending).ToList();
        }

        public void CancelRenovation(int renovationId) {
            if (!CanBeCancelled(renovationId)) {
                return;
            }

            AccommodationRenovation accommodationRenovation = this.GetById(renovationId);
            accommodationRenovation.RenovationState = RenovationState.Cancelled;
            this.Update(accommodationRenovation);
        }
        
        public bool CanBeCancelled(int renovationId) {
            AccommodationRenovation renovation = this.GetById(renovationId);
            return renovation.RenovationState == RenovationState.Pending &&
                renovation.StartDate > DateOnly.FromDateTime(DateTime.Now).AddDays(5);
        }
        
        public List<AccommodationRenovation> GetByOwnerIdSorted(int ownerId) {
            List<AccommodationRenovation> renovations = new List<AccommodationRenovation> { };
            AccommodationService accService = ServicesPool.GetService<AccommodationService>();

            foreach (var accommodation in accService.GetByOwnerId(ownerId)) {
                renovations.AddRange(this.GetByAccommodationId(accommodation.Id));
            }

            return renovations.OrderBy(x => x.RenovationState).ToList();
        }
        
        public bool CheckAccommodationAvailability(int accommodationId, DateOnly startDate, DateOnly endDate) {
            foreach (var r in this.GetPendingByAccommodationId(accommodationId)) {
                if (!(r.StartDate >= endDate || startDate >= r.EndDate))
                    return false;
            }

            return true;
        }
        
        public List<AccommodationRenovation> GetAvaliableRenovationDates(int accommodationId, DateOnly startBorderDate, DateOnly endBorderDate, int duration) {
            List<AccommodationRenovation> availableDates = new List<AccommodationRenovation>();

            while (startBorderDate.AddDays(duration) <= endBorderDate) {
                DateOnly startDate = startBorderDate;
                DateOnly endDate = startDate.AddDays(duration);

                if (this.CheckAccommodationAvailability(accommodationId, startDate, endDate) &&
                    _accommodationReservationService.CheckAccommodationAvailability(accommodationId,startDate,endDate)) {
                    AccommodationRenovation accommodationRenovation = new AccommodationRenovation(accommodationId, startDate, endDate,
                        RenovationState.Pending, "");

                    availableDates.Add(accommodationRenovation);
                }

                startBorderDate = startBorderDate.AddDays(1);
            }

            return availableDates;
        }
        
        public void UpdateAllPendingRenovations() {
            foreach (var renovation in this.GetAllPendingRenovations()) {
                if (renovation.EndDate < DateOnly.FromDateTime(DateTime.Now)) {
                    renovation.RenovationState = RenovationState.Finished;
                    this.Update(renovation);
                }
            }
        }
    }
}
