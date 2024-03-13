using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
public class AccommodationReservationDTO:INotifyPropertyChanged {
        public AccommodationReservationDTO() {
        }

        public AccommodationReservationDTO(AccommodationReservation acc) {
            Id = acc.Id;
            IdGuest = acc.IdGuest;
            IdAccommodation = acc.IdAccommodation;
            GuestsNumber = acc.GuestsNumber;
            StartDate = acc.StartDate;
            EndDate = acc.EndDate;
            AccommodationName = "";
            Graded = false;
        }

        public int Id { get; set; }
        public int IdGuest { get; set; }
        public int IdAccommodation { get; set; }

        public int _guestsNumber;
        public int GuestsNumber {
            get {
                return _guestsNumber;
            }
            set {
                if (value != _guestsNumber) {
                    _guestsNumber = value;
                    OnPropertyChanged();
                }
            }
        }

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
        public string AccommodationName { get; set; }
        public bool Graded { get; set; }

        public AccommodationReservation ToAccommodationReservation() {
            return new AccommodationReservation();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
