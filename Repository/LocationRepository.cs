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
    public class LocationRepository
    {
        private const string FilePath = "../../../Resources/Data/location.csv";

        private readonly Serializer<Location> _serializer;

        private List<Location> _locations;

        public LocationRepository()
        {
            _serializer = new Serializer<Location>();
            _locations = _serializer.FromCSV(FilePath);
        }

        public List<Location> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Location Save(Location location)
        {
            location.Id = NextId();
            _locations = _serializer.FromCSV(FilePath);
            _locations.Add(location);
            _serializer.ToCSV(FilePath, _locations);
            return location;
        }

        public int NextId()
        {
            _locations = _serializer.FromCSV(FilePath);
            if (_locations.Count < 1)
            {
                return 1;
            }
            return _locations.Max(c => c.Id) + 1;
        }

        public bool Delete(Location location)
        {
            _locations = _serializer.FromCSV(FilePath);
            Location? found = _locations.Find(c => c.Id == location.Id);
            if (found == null)
            {
                return false;
            }
            _locations.Remove(found);
            _serializer.ToCSV(FilePath, _locations);
            return true;
        }
        public Location GetById(int id)
        {
            _locations = _serializer.FromCSV(FilePath);
            Location location = _locations.Find(c => c.Id == id);
            return location;
        }

        public bool Update(Location location)
        {
            _locations = _serializer.FromCSV(FilePath);
            Location current = _locations.Find(c => c.Id == location.Id);
            int index = _locations.IndexOf(current);
            if (current == null)
            {
                return false;
            }
            _locations.Remove(current);
            _locations.Insert(index, location);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _locations);
            return true;
        }
    }
}
