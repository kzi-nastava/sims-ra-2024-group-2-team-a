using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class VisitedTourService {
        private readonly IVisitedTourRepository _visitedTourRepository;
        
        private VoucherService _voucherService;
        private TourService _tourService;

        public VisitedTourService(IVisitedTourRepository repository) { 
            _visitedTourRepository = repository;
        }

        public void InjectServices(VoucherService voucherService, TourService tourService) {
            _voucherService = voucherService;
            _tourService = tourService;
        }

        private bool IsOutdated(VisitedTour visitedTour, Tour currentTour) {
            return (visitedTour.Date < currentTour.Beggining.AddYears(-1));
        }

        public void Save(int touristId, int tourId) {
            Tour currentTour = _tourService.GetById(tourId);
            _visitedTourRepository.Save(new VisitedTour(touristId, currentTour.Beggining));
            CheckForVoucher(touristId, currentTour);
        }

        private void CheckForVoucher(int touristId, Tour currentTour) {
            if (CountVisitedTours(touristId, currentTour) == 5) {
                _visitedTourRepository.DeleteAllByTouristId(touristId);
                _voucherService.AwardVoucher(touristId);
            }
        }

        private int CountVisitedTours(int touristId, Tour currentTour) {
            int visitedTourCounter = 0;

            foreach (var visitedTour in _visitedTourRepository.GetByTouristId(touristId)) {
                if (IsOutdated(visitedTour, currentTour)) {
                    _visitedTourRepository.Delete(visitedTour);
                    continue;
                }

                visitedTourCounter++;
            }

            return visitedTourCounter;
        }
    }
}
