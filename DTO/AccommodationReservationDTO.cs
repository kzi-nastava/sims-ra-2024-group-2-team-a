﻿using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class AccommodationReservationDTO : INotifyPropertyChanged {

        public AccommodationReservationDTO() {
        }

        public AccommodationReservationDTO(AccommodationReservation acc) {
            Id = acc.Id;
            GuestId = acc.GuestId;
            AccommodationId = acc.AccommodationId;
            GuestsNumber = acc.GuestsNumber;
            StartDate = acc.StartDate;
            EndDate = acc.EndDate;
            Graded = false;
        }

        public AccommodationReservationDTO(AccommodationReservationDTO accDTO) {
            Id = accDTO.Id;
            GuestId = accDTO.GuestId;
            AccommodationId = accDTO.AccommodationId;
            GuestsNumber = accDTO.GuestsNumber;
            StartDate = accDTO.StartDate;
            EndDate = accDTO.EndDate;
            ReservationDays = accDTO.ReservationDays;
            AccommodationName = accDTO.AccommodationName;
            Graded = accDTO.Graded;
        }

        public int Id { get; set; }
        public int GuestId { get; set; }
        public int AccommodationId { get; set; }

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
        public AccommodationType AccommodationType { get; set; }
        public string AccommodationLocation { get; set; }
        public int LastCancellationDay { get; set; }
        public String CancellationDate => StartDate.AddDays(-LastCancellationDay).ToString();
        public bool Graded { get; set; }
        public int ReservationDays { get; set; }
        public string DateString => $"{StartDate}\n{EndDate}";

        public bool HasExpired {
            get {
                var dateNow = DateOnly.FromDateTime(DateTime.Now);
                return dateNow > EndDate;
            }
        }

        public bool CanBeGraded {
            get {
                var dateNow = DateOnly.FromDateTime(DateTime.Now);
                return dateNow <= EndDate.AddDays(5) && dateNow > EndDate;
            }
        }

        public bool CanBeRescheduled {
            get {
                var dateNow = DateOnly.FromDateTime(DateTime.Now);
                return dateNow <= StartDate;
            }
        }

        public bool CanBeCancelled {
            get {
                var dateNow = DateOnly.FromDateTime(DateTime.Now);
                return dateNow <= StartDate.AddDays(-LastCancellationDay);
            }
        }

        public AccommodationReservation ToAccommodationReservation() {
            return new AccommodationReservation();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
