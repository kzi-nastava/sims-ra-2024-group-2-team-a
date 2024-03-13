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
            List<AccommodationReservation> accommodationReservations = new List<AccommodationReservation> ();

            foreach (var accRes in _serializer.FromCSV()) {
                if(accRes.IdAccommodation == id) {
                    accommodationReservations.Add(accRes);
                }
            }
            return accommodationReservations;
        }
    }
}
