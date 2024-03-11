using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BookingApp.Repository {
    public class AccommodationRepository : IRepository<Accommodation> {

        private readonly Serializer<Accommodation> _serializer;

        private List<Accommodation> _accommodations;

        public AccommodationRepository() {
            _serializer = new Serializer<Accommodation>();
            _accommodations = _serializer.FromCSV();
        }

        public List<Accommodation> GetAll() {
            return _serializer.FromCSV();
        }

        public Accommodation Save(Accommodation accommodation) {
            accommodation.Id = NextId();
            _accommodations = _serializer.FromCSV();
            _accommodations.Add(accommodation);
            _serializer.ToCSV(_accommodations);
            return accommodation;
        }

        public bool Delete(Accommodation accommodation) {
            _accommodations = _serializer.FromCSV();
            Accommodation? found = _accommodations.Find(c => c.Id == accommodation.Id);
            if (found == null) {
                return false;
            }
            _accommodations.Remove(found);
            _serializer.ToCSV(_accommodations);
            return true;
        }

        public bool Update(Accommodation accommodation) {
            _accommodations = _serializer.FromCSV();
            Accommodation current = _accommodations.Find(c => c.Id == accommodation.Id);
            int index = _accommodations.IndexOf(current);
            if (current == null) {
                return false;
            }
            _accommodations.Remove(current);
            _accommodations.Insert(index, accommodation);       // keep ascending order of ids in file 
            _serializer.ToCSV(_accommodations);
            return true;
        }

        public Accommodation GetById(int id) {
            return _accommodations.Find(x => x.Id == id);
        }

        public int NextId() {
            _accommodations = _serializer.FromCSV();
            if (_accommodations.Count < 1) {
                return 1;
            }
            return _accommodations.Max(c => c.Id) + 1;
        }

        public List<Accommodation> GetByOwnerId(int ownerId)
        {
            List<Accommodation> list = new List<Accommodation>();
            _accommodations = _serializer.FromCSV();
            foreach (var acc in _accommodations)
            {
                if (acc.OwnerId == ownerId)
                    list.Add(acc);
            }

            return list;
        }
    }
}