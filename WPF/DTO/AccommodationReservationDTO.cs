using BookingApp.Domain.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.DTO {
    public class AccommodationReservationDTO : INotifyPropertyChanged
    {

        public AccommodationReservationDTO() { }

        public AccommodationReservationDTO(AccommodationReservationDTO accDTO)
        {
            Id = accDTO.Id;
            GuestId = accDTO.GuestId;
            AccommodationId = accDTO.AccommodationId;
            GuestsNumber = accDTO.GuestsNumber;
            StartDate = accDTO.StartDate;
            EndDate = accDTO.EndDate;
            ReservationDays = accDTO.ReservationDays;
            IsGradedByGuest = accDTO.IsGradedByGuest;
            IsGradedByOwner = accDTO.IsGradedByOwner;
            Cancelled = accDTO.Cancelled;
        }

        public AccommodationReservationDTO(AccommodationReservation acc)
        {
            Id = acc.Id;
            GuestId = acc.GuestId;
            AccommodationId = acc.AccommodationId;
            GuestsNumber = acc.GuestsNumber;
            StartDate = acc.StartDate;
            EndDate = acc.EndDate;
            ReservationDays = acc.ReservationDays;
            Cancelled = acc.Cancelled;
        }

        public int Id { get; set; } = 0;
        public int GuestId { get; set; } = 0;
        public int AccommodationId { get; set; } = 0;

        public int _guestsNumber;
        public int GuestsNumber
        {
            get
            {
                return _guestsNumber;
            }
            set
            {
                if (value != _guestsNumber)
                {
                    _guestsNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateOnly _startDate;
        public DateOnly StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateOnly _endDate;
        public DateOnly EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _cancelled;
        public bool Cancelled
        {
            get
            {
                return _cancelled;
            }
            set
            {
                if (value != _cancelled)
                {
                    _cancelled = value;
                    OnPropertyChanged();
                }
            }
        }

        public AccommodationDTO Accommodation { get; set; }
        public DateOnly CancellationDate => StartDate.AddDays(-Accommodation.LastCancellationDay);
        public bool IsGradedByGuest { get; set; } = false;
        public bool IsGradedByOwner { get; set; } = false;
        public int ReservationDays { get; set; }

        public bool CanBeViewedByOwner {
            get {
                return IsGradedByGuest && IsGradedByOwner;
            }
        }
        public bool CanBeGradedByOwner {
            get {
                var dateNow = DateOnly.FromDateTime(DateTime.Now);
                return dateNow <= EndDate.AddDays(5) && dateNow > EndDate && !Cancelled && !IsGradedByOwner;
            }
        }

        public bool HasExpired
        {
            get
            {
                var dateNow = DateOnly.FromDateTime(DateTime.Now);
                return dateNow > EndDate;
            }
        }

        public bool CanBeGraded
        {
            get
            {
                var dateNow = DateOnly.FromDateTime(DateTime.Now);
                return dateNow <= EndDate.AddDays(5) && dateNow > EndDate && !Cancelled && !IsGradedByGuest;
            }
        }

        public bool CanBeRescheduled
        {
            get
            {
                var dateNow = DateOnly.FromDateTime(DateTime.Now);
                return dateNow <= StartDate && !Cancelled;
            }
        }

        public bool CanBeCancelled
        {
            get
            {
                var dateNow = DateOnly.FromDateTime(DateTime.Now);
                return dateNow <= StartDate.AddDays(-Accommodation.LastCancellationDay) && !Cancelled;
            }
        }

        public bool IsReservationFinished {
            get {
                var dateNow = DateOnly.FromDateTime(DateTime.Now);
                return dateNow >= EndDate;
            }
        }

        public AccommodationReservation ToAccommodationReservation()
        {
            var res = new AccommodationReservation(GuestId, AccommodationId, GuestsNumber, StartDate, EndDate);
            res.Id = Id;
            return res;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)

        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
