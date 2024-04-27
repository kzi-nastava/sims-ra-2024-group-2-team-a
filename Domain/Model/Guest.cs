using System;

namespace BookingApp.Domain.Model {

    public class Guest : User {

        public static readonly int superGuestReservationsCount = 10;

        public bool IsSuperGuest { get; set; }

        public DateOnly SuperGuestExpirationDate { get; set; }

        public int BonusPoints { get; set; }

        public Guest() { }

        public Guest(string username, string password) : base(username, password) {
            Category = UserCategory.Guest;
            IsSuperGuest = false;
        }


        public string[] ToCSV() {
            string[] csvValues = { 
                Id.ToString(), 
                IsSuperGuest.ToString(),
                SuperGuestExpirationDate.ToString(),
                BonusPoints.ToString()
            };
            return csvValues;
        }


        public void FromCSV(string[] values) {
            base.FromCSV(values);
            IsSuperGuest = Convert.ToBoolean(values[^3]);
            SuperGuestExpirationDate = DateOnly.Parse(values[^2]);
            BonusPoints = Convert.ToInt32(values[^1]);
        }
    }
}
