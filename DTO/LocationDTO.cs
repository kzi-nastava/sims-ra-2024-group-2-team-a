using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BookingApp.Model;

namespace BookingApp.DTO
{
    public class LocationDTO : INotifyPropertyChanged
    {
        private int _id = -1;
        public int Id {
            get {
                return _id;
            }
            set {
                if (value != _id) {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public LocationDTO() { }
        public LocationDTO(string city, string country) {
            _city = city;
            _country = country;
        }

        public LocationDTO(Location location) {
            Id = location.Id;
            City = location.City;
            Country = location.Country;
        }

        private string _city = "";
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _country = "";
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LocationOptionTemplate => Id != -1 ? $"{Country} - {City}" : "" ;
 
        public Location ToLocation()
        {
            return new Location(_city, _country);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
