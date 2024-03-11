using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.DTO
{
    public class AccommodationDTO : INotifyPropertyChanged
    {
        public AccommodationDTO() {
            ProfilePictures = new List<string>();
        }
        public AccommodationDTO(string name, int locationId, AccommodationType accomodationType,
            int maxguestNumber, int minReservationDays, int cancellationDate, int ownerId, List<string> profilePictures)
        {
            Name = name;
            LocationId = locationId;
            Type = accomodationType;
            MaxGuestNumber = maxguestNumber;
            MinReservationDays = minReservationDays;
            LastCancellationDay = cancellationDate;
            OwnerId = ownerId;
            ProfilePictures = profilePictures;
        }

        public AccommodationDTO(Accommodation acc)
        {
            Name = acc.Name;
            LocationId = acc.LocationId;
            Type = acc.type;
            MaxGuestNumber = acc.MaxGuestNumber;
            MinReservationDays = acc.MinReservationDays;
            LastCancellationDay = acc.LastCancellationDay;
            OwnerId = acc.OwnerId;
            ProfilePictures = acc.ProfilePictures;
        }

        private string _name;
        public string Name {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _locationId;
        public int LocationId {
            get
            {
                return _locationId;
            }
            set
            {
                if (value != _locationId)
                {
                    _locationId = value;
                    OnPropertyChanged();
                }
            }
        }

        public AccommodationType _type;
        public AccommodationType Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _maxGuestNumber;
        public int MaxGuestNumber {
            get
            {
                return _maxGuestNumber;
            }
            set
            {
                if (value != _maxGuestNumber)
                {
                    _maxGuestNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _minReservationDays;
        public int MinReservationDays {
            get
            {
                return _minReservationDays;
            }
            set
            {
                if (value != _minReservationDays)
                {
                    _minReservationDays = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _lastCancellationDay;
        public int LastCancellationDay
        {
            get
            {
                return _lastCancellationDay;
            }
            set
            {
                if (value != _lastCancellationDay)
                {
                    _lastCancellationDay = value;
                    OnPropertyChanged();
                }
            }
        }
        public int OwnerId { get; set; }
        public List<string> ProfilePictures { get; set; }

        /*private string _profilePicturesNotParsed;
        public string ProfilePicturesNotParsed
        {
            get
            {
                return _profilePicturesNotParsed;
            }
            set
            {
                if (value != _profilePicturesNotParsed)
                {
                    _profilePicturesNotParsed = value;
                    OnPropertyChanged();
                }
            }
        }/*

        /*public void ParseProfilePictures()
        {
            string[] parsedStrings = _profilePicturesNotParsed.Split(',');
            foreach(string s in parsedStrings)
            {
                ProfilePictures.Add(s);
            }
        }*/

        public string DisplayLocation { get; set; }

        public void SetDisplayLocation(string city,string country)
        {
            DisplayLocation = $"{country}, {city}";
        }

        public Accommodation ToAccommodation()
        {
            Accommodation accomodation = new Accommodation(Name, LocationId,Type,MaxGuestNumber,MinReservationDays, LastCancellationDay, OwnerId, ProfilePictures);
            //ParseProfilePictures();
            return accomodation;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
