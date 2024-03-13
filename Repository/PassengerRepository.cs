using BookingApp.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository {
    public class PassengerRepository : IRepository<Passenger> {

        private readonly Serializer<Passenger> _serializer;

        private List<Passenger> _passengers;

        public PassengerRepository() {
            _serializer = new Serializer<Passenger>();
            _passengers = _serializer.FromCSV();
        }

        public List<Passenger> GetAll() {
            return _serializer.FromCSV();
        }

        public Passenger Save(Passenger passenger) {
            passenger.Id = NextId();
            _passengers = _serializer.FromCSV();
            _passengers.Add(passenger);
            _serializer.ToCSV(_passengers);
            return passenger;
        }

        public bool Delete(Passenger passenger) {
            _passengers = _serializer.FromCSV();
            Passenger? found = _passengers.Find(c => c.Id == passenger.Id);
            if (found == null) {
                return false;
            }
            _passengers.Remove(found);
            _serializer.ToCSV(_passengers);
            return true;
        }

        public bool Update(Passenger passenger) {
            _passengers = _serializer.FromCSV();
            Passenger? current = _passengers.Find(c => c.Id == passenger.Id);
            int index = _passengers.IndexOf(current);
            if (current == null) {
                return false;
            }
            _passengers.Remove(current);
            _passengers.Insert(index, passenger);       // keep ascending order of ids in file 
            _serializer.ToCSV(_passengers);
            return true;
        }

        public Passenger GetById(int id) {
            return _passengers.Find(x => x.Id == id);
        }

        public int NextId() {
            _passengers = _serializer.FromCSV();
            if (_passengers.Count < 1) {
                return 1;
            }
            return _passengers.Max(c => c.Id) + 1;
        }
    }
}
