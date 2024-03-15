using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    class AccommodationReservationRepository : IRepository<AccommodationReservation> {

        private readonly Serializer<AccommodationReservation> _serializer;

        private List<AccommodationReservation> _accommodationReservations;

        public AccommodationReservationRepository() {
            _serializer = new Serializer<AccommodationReservation>();
        }

        public bool Delete(AccommodationReservation item) {
            throw new NotImplementedException();
        }

        public List<AccommodationReservation> GetAll() {
            return _serializer.FromCSV();
        }

        public AccommodationReservation GetById(int id) {
            _accommodationReservations = _serializer.FromCSV();
            AccommodationReservation reservation = _accommodationReservations.Find(c => c.Id == id);

            return reservation;
        }

        public AccommodationReservation Save(AccommodationReservation item) {
            item.Id = NextId();
            _accommodationReservations = _serializer.FromCSV();
            _accommodationReservations.Add(item);
            _serializer.ToCSV(_accommodationReservations);
            return item;
        }

        public bool Update(AccommodationReservation item) {
            AccommodationReservation reservation = this.GetById(item.Id);

            if (reservation == null)
                return false;

            int index = _accommodationReservations.IndexOf(reservation);
            _accommodationReservations.Remove(reservation);
            _accommodationReservations.Insert(index, reservation);

            _serializer.ToCSV(_accommodationReservations);
            return true;
        }

        public int NextId() {
            _accommodationReservations = _serializer.FromCSV();
            if (_accommodationReservations.Count < 1)
                return 1;

            return _accommodationReservations.Max(c => c.Id) + 1;
        }

        public List<AccommodationReservation> GetByAccommodationId(int id) {
            List<AccommodationReservation> accommodationReservations = new List<AccommodationReservation>();

            foreach (var accRes in _serializer.FromCSV()) {
                if(accRes.IdAccommodation == id) {
                    accommodationReservations.Add(accRes);
                }
            }
            return accommodationReservations;
        }

        public List<AccommodationReservation> GetPossibleReservations(int idAccommodation, DateOnly startDate, DateOnly endDate, int reservationsDays) {

            List<AccommodationReservation> exisitingReservations = GetByAccommodationId(idAccommodation);
            var reservationPool = exisitingReservations.Where(r => r.StartDate >= startDate && r.EndDate <= endDate).ToList();

            while(GeneratePossibleReservationFirstFit(reservationPool, startDate, endDate, reservationsDays)) { }

            var possibleReservations = reservationPool.Where(r => r.Id == -1).ToList();

            // TODO: Add suggested reservations path

            return possibleReservations;
        }
        
        public bool DoReservationsOverlap(AccommodationReservation r1, AccommodationReservation r2) {
            if (r1.StartDate >= r2.EndDate || r2.StartDate >= r1.EndDate)
                return false;

            return true;
        }

        // Returns true if it found slot for reservation and adds it to reservations list
        public bool GeneratePossibleReservationFirstFit(List<AccommodationReservation> reservations, DateOnly startDate, DateOnly endDate, int reservationDays) {

            List<DateOnly> endDates = reservations.Select(r => r.EndDate).ToList();
            endDates = endDates.OrderBy(d => d).ToList();
            endDates.Add(startDate);

            foreach (var date in endDates) {
                var possibleReservation = new AccommodationReservation(0, 0, 0, date, date.AddDays(reservationDays));

                if (!reservations.Any(r => DoReservationsOverlap(r, possibleReservation)) && possibleReservation.EndDate <= endDate) {
                    reservations.Add(possibleReservation);
                    return true;
                }
            }

            return false;
        }
    }
}
