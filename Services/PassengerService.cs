﻿using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class PassengerService {
        private readonly PassengerRepository _passengerRepository = new PassengerRepository();

        public List<int> GetAttendance(List<TourReservation> reservations) {
            List<Passenger> passengers = GetByReservations(reservations);
            List<int> attendances = new List<int>();
            attendances.Add(_passengerRepository.GetStatsTeen(passengers));
            attendances.Add(_passengerRepository.GetStatsMid(passengers));
            attendances.Add(_passengerRepository.GetStatsOld(passengers));
            return attendances;
        }
        public List<Passenger> GetByReservations(List<TourReservation> reservations) {
            return _passengerRepository.GetByReservations(reservations);
        }
        public bool DeleteByReservations(List<TourReservation> reservations) {
            return _passengerRepository.DeleteMultiple(GetByReservations(reservations));
        }
        public Passenger? GetByReservationAndTourist(int reservationId, int touristId) {
            return _passengerRepository.GetByReservationAndTourist(reservationId, touristId);
        }
        public List<Passenger> GetUnJoined(List<TourReservation> reservations) {
            return _passengerRepository.GetUnJoined(reservations);
        }

        public bool Update(Passenger? passenger) {
            return _passengerRepository.Update(passenger);
        }
    }
}