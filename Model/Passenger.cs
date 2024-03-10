using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model {
    public class Passenger : ISerializable {
        public int Id { get; set; }
        public int TourId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }

        public Passenger() { }

        public Passenger(int id, int tourId, string username, string password, int age) {
            Id = id;
            TourId = tourId;
            Username = username;
            Password = password;
            Age = age;
        }

        public string[] ToCSV() {
            string[] csvValues = { Id.ToString(), TourId.ToString(), Username, Password, Age.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            Username = values[2];
            Password = values[3];
            Age = Convert.ToInt32(values[4]);
        }
    }
}

