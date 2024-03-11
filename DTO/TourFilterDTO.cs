using BookingApp.Model;
using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace BookingApp.DTO {
    public class TourFilterDTO {
        public TourFilterDTO() { }

        public TourFilterDTO(LocationDTO location, double duration, string language, int touristNumber) {
            Location = location;
            Duration = duration;
            Language = language;
            TouristNumber = touristNumber;
        }

        private LocationDTO _location;
        public LocationDTO Location {
            get {
                return _location;
            }
            set {
                if (_location != value) {
                    _location = value;
                }
            }
        }

        private string _language;
        public string Language {
            get {
                return _language;
            }
            set {
                if (_language != value) {
                    _language = value;
                }
            }
        }

        private int _touristNumber;
        public int TouristNumber {
            get {
                return _touristNumber;
            }
            set {
                if (value != _touristNumber) {
                    _touristNumber = value;
                }
            }
        }

        private double _duration;
        public double Duration {
            get {
                return _duration;
            }
            set {
                if (_duration != value) {
                    _duration = value;
                }
            }
        }
        public bool isEmpty() {           
            return Location == null && Language == "" && Duration == 0 && TouristNumber == 0;
        }                       
    }                                                    
}