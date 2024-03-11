using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BookingApp.Repository {
    public class AccomodationRepository : IRepository<Accomodation> {

        private readonly Serializer<Accomodation> _serializer;

        private List<Accomodation> _accomodations;

        public AccomodationRepository() {
            _serializer = new Serializer<Accomodation>();
            _accomodations = _serializer.FromCSV();
        }

        public List<Accomodation> GetAll() {
            return _serializer.FromCSV();
        }

        public Accomodation Save(Accomodation accomodation) {
            accomodation.Id = NextId();
            _accomodations = _serializer.FromCSV();
            _accomodations.Add(accomodation);
            _serializer.ToCSV(_accomodations);
            return accomodation;
        }

        public bool Delete(Accomodation accomodation) {
            _accomodations = _serializer.FromCSV();
            Accomodation? found = _accomodations.Find(c => c.Id == accomodation.Id);
            if (found == null) {
                return false;
            }
            _accomodations.Remove(found);
            _serializer.ToCSV(_accomodations);
            return true;
        }

        public bool Update(Accomodation accomodation) {
            _accomodations = _serializer.FromCSV();
            Accomodation current = _accomodations.Find(c => c.Id == accomodation.Id);
            int index = _accomodations.IndexOf(current);
            if (current == null) {
                return false;
            }
            _accomodations.Remove(current);
            _accomodations.Insert(index, accomodation);       // keep ascending order of ids in file 
            _serializer.ToCSV(_accomodations);
            return true;
        }

        public Accomodation GetById(int id) {
            return _accomodations.Find(x => x.Id == id);
        }

        public int NextId() {
            _accomodations = _serializer.FromCSV();
            if (_accomodations.Count < 1) {
                return 1;
            }
            return _accomodations.Max(c => c.Id) + 1;
        }

        public List<Accomodation> GetByOwnerId(int ownerId)
        {
            List<Accomodation> list = new List<Accomodation>();
            _accomodations = _serializer.FromCSV();
            foreach (var acc in _accomodations)
            {
                if (acc.OwnerId == ownerId)
                    list.Add(acc);
            }

            return list;
        }
    }
}