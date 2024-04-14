using BookingApp.Domain.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.DTO {
    public class PointOfInterestDTO : INotifyPropertyChanged
    {
        public PointOfInterestDTO() { }

        public PointOfInterestDTO(string name, string description, bool isChecked, int tourId)
        {
            Name = name;
            Description = description;
            IsChecked = isChecked;
            TourId = tourId;
        }
        public PointOfInterestDTO(PointOfInterest p)
        {
            Id = p.Id;
            Name = p.Name;
            Description = p.Description;
            IsChecked = p.IsChecked;
            TourId = p.TourId;
        }
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _tourId;
        public int TourId
        {
            get { return _tourId; }
            set
            {
                if (_tourId != value)
                {
                    _tourId = value;
                    OnPropertyChanged();
                }
            }
        }

        public PointOfInterest ToModelNoId()
        {
            return new PointOfInterest(Name, Description, IsChecked, TourId);
        }

        public PointOfInterest ToModel()
        {
            return new PointOfInterest(Id, Name, Description, IsChecked, TourId);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
