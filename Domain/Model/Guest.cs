using System;

namespace BookingApp.Domain.Model {

    public class Guest : User
    {

        public bool isSuperGuest { get; set; }

        public Guest() { }

        public Guest(string username, string password) : base(username, password)
        {
            Category = UserCategory.Guest;
            isSuperGuest = false;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), isSuperGuest.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            base.FromCSV(values);
            isSuperGuest = Convert.ToBoolean(values[^1]);
        }
    }
}
