using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class ReservationRecommenderService {

        private readonly IAccommodationReservationRepository _reservationRepository = RepositoryInjector.GetInstance<IAccommodationReservationRepository>();

        private readonly int daysSpan = 20;

        public List<AccommodationReservation> SuggestReservations(AccommodationReservationDTO rDTO) {
            var possibleReservations = GetPossibleReservations(rDTO);

            if (possibleReservations.Count > 0)
                return possibleReservations;

            // Suggest other reservations that are not in given data range
            var rBeforeDTO = new AccommodationReservationDTO(rDTO);
            rBeforeDTO.StartDate = rDTO.StartDate.AddDays(-daysSpan);
            rBeforeDTO.EndDate = rDTO.StartDate;
            var reservationsBefore = GetPossibleReservations(rBeforeDTO);

            var rAfterDTO = new AccommodationReservationDTO(rDTO);
            rAfterDTO.StartDate = rDTO.EndDate;
            rAfterDTO.EndDate = rDTO.EndDate.AddDays(daysSpan);
            var reservationsAfter = GetPossibleReservations(rAfterDTO);

            possibleReservations.AddRange(reservationsBefore);
            possibleReservations.AddRange(reservationsAfter);

            return possibleReservations;
        }

        private List<AccommodationReservation> GetPossibleReservations(AccommodationReservationDTO rDTO) {

            List<AccommodationReservation> exisitingReservations = _reservationRepository.GetByAccommodationId(rDTO.AccommodationId);
            var reservationPool = exisitingReservations.Where(r => IsReservationInDateRange(r, rDTO.StartDate, rDTO.EndDate)).ToList();

            while (GeneratePossibleReservationFirstFit(reservationPool, rDTO)) { }

            // Filter out reservations that are created but not yet saved to file
            var possibleReservations = reservationPool.Where(r => r.Id == -1).ToList();

            return possibleReservations;
        }

        private bool DoReservationsOverlap(AccommodationReservation r1, AccommodationReservation r2) {
            if (r1.StartDate >= r2.EndDate || r2.StartDate >= r1.EndDate)
                return false;

            return true;
        }

        private bool IsReservationInDateRange(AccommodationReservation r, DateOnly start, DateOnly end) {
            return r.StartDate >= start && r.EndDate <= end;
        }

        // Returns true if it found slot for reservation and adds it to reservations list
        private bool GeneratePossibleReservationFirstFit(List<AccommodationReservation> reservations, AccommodationReservationDTO rDTO) {

            List<DateOnly> endDates = reservations.Select(r => r.EndDate).ToList();
            endDates.Add(rDTO.StartDate);
            endDates = endDates.OrderBy(d => d).ToList();

            foreach (var date in endDates) {
                var possibleReservation = new AccommodationReservation(0, rDTO.AccommodationId, rDTO.GuestsNumber, date, date.AddDays(rDTO.ReservationDays));

                if (!reservations.Any(r => DoReservationsOverlap(r, possibleReservation)) && possibleReservation.EndDate <= rDTO.EndDate) {
                    reservations.Add(possibleReservation);
                    return true;
                }
            }

            return false;
        }
    }
}
