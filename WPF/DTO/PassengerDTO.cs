using BookingApp.Domain.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.DTO {
    public class PassengerDTO : INotifyPropertyChanged
    {
        public PassengerDTO() { }
        public PassengerDTO(string name, string surname, int age, int userId)
        {
            Name = name;
            Surname = surname;
            Age = age;
            //JoinedPointOfInterestId
            UserId = userId;
        }
        public PassengerDTO(Passenger p)
        {
            Id = p.Id;
            TourReservationId = p.TourReservationId;
            Name = p.Name;
            Surname = p.Surname;
            Age = p.Age;
            JoinedPointOfInterestId = p.JoinedPointOfInterestId;
            UserId = p.UserId;

        }

        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _tourReservationId;
        public int TourReservationId
        {
            get
            {
                return _tourReservationId;
            }
            set
            {
                if (_tourReservationId != value)
                {
                    _tourReservationId = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _surname;
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _age;
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _isJoined;
        public bool IsJoined
        {
            get
            {
                return _isJoined;
            }
            set
            {
                if (_isJoined != value)
                {
                    _isJoined = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _joinedPointOfInterestId;
        public int JoinedPointOfInterestId
        {
            get
            {
                return _joinedPointOfInterestId;
            }
            set
            {
                if (_joinedPointOfInterestId != value)
                {
                    _joinedPointOfInterestId = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _joinedPointOfInterestName;
        public string JoinedPointOfInterestName
        {
            get
            {
                return _joinedPointOfInterestName;
            }
            set
            {
                if (_joinedPointOfInterestName != value)
                {
                    _joinedPointOfInterestName = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _userId;
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    OnPropertyChanged();
                }
            }
        }
        public void SetPointOfInterestName(string pointOfInterestName)
        {
            JoinedPointOfInterestName = pointOfInterestName;
        }
        public Passenger ToModel()
        {
            return new Passenger(Id, TourReservationId, Name, Surname, Age, JoinedPointOfInterestId, UserId);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)

        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
