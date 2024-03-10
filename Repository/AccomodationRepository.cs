using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BookingApp.Repository
{
    public class AccomodationRepository
    {

        private const string FilePath = "../../../Resources/Data/accomodation.csv";

        private readonly Serializer<Accomodation> _serializer;

        private List<Accomodation> _accomodations;

        public AccomodationRepository()
        {
            _serializer = new Serializer<Accomodation>();
            _accomodations = _serializer.FromCSV(FilePath);
        }

        public Accomodation GetById(int id)
        {
            return _accomodations.Find(x => x.Id == id);
        }
        public List<Accomodation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<Accomodation> GetByOwnerId(int ownerId)
        {
            List<Accomodation> list = new List<Accomodation> ();
            _accomodations = _serializer.FromCSV(FilePath);
            foreach (var acc in _accomodations)
            {
                if (acc.OwnerId == ownerId)
                    list.Add(acc);
            }

            return list;
        }

        public Accomodation Save(Accomodation accomodation)
        {
            accomodation.Id = NextId();
            _accomodations = _serializer.FromCSV(FilePath);
            _accomodations.Add(accomodation);
            _serializer.ToCSV(FilePath, _accomodations);
            return accomodation;
        }

        public int NextId()
        {
            _accomodations = _serializer.FromCSV(FilePath);
            if (_accomodations.Count < 1)
            {
                return 1;
            }
            return _accomodations.Max(c => c.Id) + 1;
        }

        public bool Delete(Accomodation accomodation)
        {
            _accomodations = _serializer.FromCSV(FilePath);
            Accomodation? found = _accomodations.Find(c => c.Id == accomodation.Id);
            if (found == null)
            {
                return false;
            }
            _accomodations.Remove(found);
            _serializer.ToCSV(FilePath, _accomodations);
            return true;
        }

        public bool Update(Accomodation accomodation)
        {
            _accomodations = _serializer.FromCSV(FilePath);
            Accomodation current = _accomodations.Find(c => c.Id == accomodation.Id);
            int index = _accomodations.IndexOf(current);
            if(current == null)
            {
                return false;
            }
            _accomodations.Remove(current);
            _accomodations.Insert(index, accomodation);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _accomodations);
            return true;
        }
    }
}
