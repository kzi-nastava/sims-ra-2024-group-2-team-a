using BookingApp.Domain.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.DTO {

    public class LocationDTO : INotifyPropertyChanged
    {

        public LocationDTO() { }
        public LocationDTO(string city, string country)
        {
            _city = city;
            _country = country;
        }
        public LocationDTO(Location location)
        {
            Id = location.Id;
            City = location.City;
            Country = location.Country;
        }

        public LocationDTO(LocationDTO location) {
            Id = location.Id;
            City = location.City;
            Country = location.Country;
        }

        public int Id { get; set; } = 0;

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

        public string LocationOptionTemplate => Id != 0 ? $"{Country} - {City}" : "";

        public string LocationInfoTemplate => Id != 0 ? $"{City}, {Country}" : "";

        public Location ToLocation()
        {
            Location loc = new Location(_city, _country);
            loc.Id = Id;
            return loc;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)

        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
