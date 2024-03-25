using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO {
    public class RescheduleRequestDTO : INotifyPropertyChanged {

        public RescheduleRequestDTO() {}

        public RescheduleRequestDTO(RescheduleRequestStatus status, int reservationId, int guestId, int ownerId, DateOnly oldStartDate, DateOnly newStartDate, int duration , string ownerComment) {
            Status = status;
            ReservationId = reservationId;
            GuestId = guestId;
            OwnerId = ownerId;
            OldStartDate = oldStartDate;
            NewStartDate = newStartDate;
            OwnerComment = ownerComment;
            Duration = duration;
        }

        public RescheduleRequestDTO(RescheduleRequest rescheduleRequest) {
            Id = rescheduleRequest.Id;
            Status = rescheduleRequest.Status;
            ReservationId = rescheduleRequest.ReservationId;
            GuestId = rescheduleRequest.GuestId;
            OwnerId = rescheduleRequest.OwnerId;
            OldStartDate = rescheduleRequest.OldStartDate;
            NewStartDate = rescheduleRequest.NewStartDate;
            OwnerComment = rescheduleRequest.OwnerComment;
            Duration = rescheduleRequest.ReservationDuration;
        }

        private int _id;
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

        private RescheduleRequestStatus _status;
        public RescheduleRequestStatus Status {
            get {
                return _status;
            }
            set {
                if (value != _status) {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _reservationId;
        public int ReservationId {
            get {
                return _reservationId;
            }
            set {
                if (value != _reservationId) {
                    _reservationId = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _guestId;
        public int GuestId {
            get {
                return _guestId;
            }
            set {
                if (value != _guestId) {
                    _guestId = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _ownerId;
        public int OwnerId {
            get {
                return _ownerId;
            }
            set {
                if (value != _ownerId) {
                    _ownerId = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateOnly _oldStartDate;
        public DateOnly OldStartDate {
            get {
                return _oldStartDate;
            }
            set {
                if (value != _oldStartDate) {
                    _oldStartDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateOnly _newStartDate;
        public DateOnly NewStartDate {
            get {
                return _newStartDate;
            }
            set {
                if (value != _newStartDate) {
                    _newStartDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateOnly _newEndDate;
        public DateOnly NewEndDate {
            get {
                return _newEndDate;
            }
            set {
                if (value != _newEndDate) {
                    _newEndDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _ownerComment;
        public string OwnerComment {
            get {
                return _ownerComment;
            }
            set {
                if (value != _ownerComment) {
                    _ownerComment = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _accommodationName;
        public string AccommodationName {
            get {
                return _accommodationName;
            }
            set {
                if (value != _accommodationName) {
                    _accommodationName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _accommodationLocation;
        public string AccommodationLocation {
            get {
                return _accommodationLocation;
            }
            set {
                if (value != _accommodationLocation) {
                    _accommodationLocation = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _oldDates;

        public string OldDates {
            get {
                return _oldDates;
            }
            set {
                if (value != _oldDates) {
                    _oldDates = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _newDates;

        public string NewDates {
            get {
                return _newDates;
            }
            set {
                if (value != _newDates) {
                    _newDates = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool _isAvailable;
        public bool IsAvailable {
            get {
                return _isAvailable;
            }
            set {
                if (value != _isAvailable) {
                    _isAvailable = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Duration;

        public void SetDates() {
            NewEndDate = NewStartDate.AddDays(Duration);
            DateOnly oldEndDate = OldStartDate.AddDays(Duration);

            OldDates = OldStartDate.ToString("dd-MM-yyyy") + "\n" + oldEndDate.ToString("dd-MM-yyyy");
            NewDates = NewStartDate.ToString("dd-MM-yyyy") + "\n" + NewEndDate.ToString("dd-MM-yyyy");
        }

        public RescheduleRequest ToRescheduleRequest() {
            RescheduleRequest rescheduleRequest = new RescheduleRequest(Status, ReservationId, GuestId, OwnerId, OldStartDate, NewStartDate, Duration ,OwnerComment);
            rescheduleRequest.Id = this.Id;
            return rescheduleRequest;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
