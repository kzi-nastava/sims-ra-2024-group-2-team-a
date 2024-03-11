using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Repository
{
    public class LocationRepository : IRepository<Location>
    {
        private readonly Serializer<Location> _serializer;

        private List<Location> _locations;

        public LocationRepository()
        {
            _serializer = new Serializer<Location>();
            _locations = _serializer.FromCSV();
        }

        public List<Location> GetAll()
        {
            return _serializer.FromCSV();
        }

        public Location Save(Location location)
        {
            location.Id = NextId();
            _locations = _serializer.FromCSV();
            _locations.Add(location);
            _serializer.ToCSV(_locations);
            return location;
        }

        public bool Delete(Location location)
        {
            _locations = _serializer.FromCSV();
            Location? found = _locations.Find(c => c.Id == location.Id);
            if (found == null)
            {
                return false;
            }
            _locations.Remove(found);
            _serializer.ToCSV(_locations);
            return true;
        }
        public Location GetById(int id)
        {
            _locations = _serializer.FromCSV();
            Location location = _locations.Find(c => c.Id == id);
            return location;
        }

        public bool Update(Location location)
        {
            _locations = _serializer.FromCSV();
            Location current = _locations.Find(c => c.Id == location.Id);
            int index = _locations.IndexOf(current);
            if (current == null)
            {
                return false;
            }
            _locations.Remove(current);
            _locations.Insert(index, location);       // keep ascending order of ids in file 
            _serializer.ToCSV(_locations);
            return true;
        }

        public int NextId() {
            _locations = _serializer.FromCSV();
            if (_locations.Count < 1) {
                return 1;
            }
            return _locations.Max(c => c.Id) + 1;
        }
    }
}
