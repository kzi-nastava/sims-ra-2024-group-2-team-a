using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.DTO {
    public class AccommodationRenovationDTO : INotifyPropertyChanged {

        public AccommodationRenovationDTO() { } 

        public AccommodationRenovationDTO(int accommodationId, DateOnly startDate, DateOnly endDate, RenovationState renovationState, AccommodationDTO accommodation, string descrpition) {
            AccommodationId = accommodationId;
            StartDate = startDate;
            EndDate = endDate;
            RenovationState = renovationState;
            Accommodation = accommodation;
            Description = descrpition;
        }

        public AccommodationRenovationDTO(AccommodationRenovation accommodationRenovation) {
            Id = accommodationRenovation.Id;
            AccommodationId = accommodationRenovation.AccommodationId;
            StartDate = accommodationRenovation.StartDate;
            EndDate = accommodationRenovation.EndDate;
            RenovationState = accommodationRenovation.RenovationState;
            Description = accommodationRenovation.Description;
        }

        public int Id { get; set; } = 0;
        public int AccommodationId { get; set; } = 0;

        private DateOnly _startDate;
        public DateOnly StartDate {
            get {
                return _startDate;
            }
            set {
                if (value != _startDate) {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateOnly _endDate;
        public DateOnly EndDate {
            get {
                return _endDate;
            }
            set {
                if (value != _endDate) {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public RenovationState RenovationState { get; set; } = RenovationState.Pending;   
        public AccommodationDTO Accommodation { get; set; }

        private string _description;
        public string Description {
            get {
                return _description;
            }
            set {
                if (value != _description) {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        public AccommodationRenovation ToAccommodationRenovation() {
            return new AccommodationRenovation(AccommodationId, StartDate, EndDate, RenovationState, Description);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
