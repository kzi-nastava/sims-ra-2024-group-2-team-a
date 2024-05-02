﻿using System;

namespace BookingApp.Domain.Model {

    public class Guest : User {

        public static readonly int SuperGuestReservationsCount = 5;
        public static readonly int SuperGuestStartPoints = 5;

        public bool IsSuperGuest { get; set; } = false;
        public DateOnly SuperGuestExpirationDate { get; set; } = new DateOnly();
        public int BonusPoints { get; set; } = 0;

        public Guest() {
            Category = UserCategory.Guest;
        }

        public Guest(User user) : base(user) {
            Category = UserCategory.Guest;
        }

        public override string[] ToCSV() {
            string[] csvValues = { 
                Id.ToString(), 
                IsSuperGuest.ToString(),
                SuperGuestExpirationDate.ToString(),
                BonusPoints.ToString()
            };
            return csvValues;
        }

        public override void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            IsSuperGuest = Convert.ToBoolean(values[1]);
            SuperGuestExpirationDate = DateOnly.Parse(values[2]);
            BonusPoints = Convert.ToInt32(values[3]);
        }
    }
}