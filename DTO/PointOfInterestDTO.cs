using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO {
    public class PointOfInterestDTO : INotifyPropertyChanged {
        public PointOfInterestDTO() { }

        public PointOfInterestDTO(string name, string description, bool isChecked, int tourId) {
            Name = name;
            Description = description;
            IsChecked = isChecked;
            TourId = tourId;
        }
        public PointOfInterestDTO(PointOfInterest p) {
            Name = p.Name;
            Description = p.Description;
            IsChecked = p.IsChecked;
            TourId = p.TourId;
        }

        private string _name;
        public string Name { 
            get { return _name; }
            set { if(_name != value) {
                    _name = value;
                    OnPropertyChanged(); 
                } 
            }
        }
        private string _description;
        public string Description {
            get { return _description; }
            set {
                if (_description != value) {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isChecked;
        public bool IsChecked {
            get { return _isChecked; }
            set {
                if (_isChecked != value) {
                    _isChecked = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _tourId;
        public int TourId {
            get { return _tourId; }
            set {
                if (_tourId != value) {
                    _tourId = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
