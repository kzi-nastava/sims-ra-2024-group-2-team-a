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

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public string[] ToCSV()
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        {
            string[] csvValues = { Id.ToString(), isSuperGuest.ToString() };
            return csvValues;
        }

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public void FromCSV(string[] values)
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        {
            base.FromCSV(values);
            isSuperGuest = Convert.ToBoolean(values[^1]);
        }
    }
}
