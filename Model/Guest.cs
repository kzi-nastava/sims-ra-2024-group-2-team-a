using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model {

    public class Guest : User {

        public bool isSuperGuest { get; set; }

        public Guest() { }

        public Guest(string username, string password) : base(username, password) {
            Category = UserCategory.Guest;
            isSuperGuest = false;
        }

        public string[] ToCSV() {
            string[] csvValues = { Id.ToString(), isSuperGuest.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values) {
            base.FromCSV(values);
            isSuperGuest = Convert.ToBoolean(values[^1]);
        }
    }
}
