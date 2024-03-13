using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model {
    public class Passenger : ISerializable {
        public int Id { get; set; }
        public int TourReservationId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int UserId { get; set; } 

        public Passenger() { }

        public Passenger(int id, int tourId, string name, string surname, int age, int userId) {
            Id = id;
            TourReservationId = tourId;
            Name = name;
            Surname = surname;
            Age = age;
            UserId = userId;
        }

        public Passenger(int tourId, string name, string surname, int age, int userId) {
            TourReservationId = tourId;
            Name = name;
            Surname = surname;
            Age = age;
            UserId = userId;
        }

        public string[] ToCSV() {
            string[] csvValues = { Id.ToString(), TourReservationId.ToString(), Name, Surname, Age.ToString(), UserId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            TourReservationId = Convert.ToInt32(values[1]);
            Name = values[2];
            Surname = values[3];
            Age = Convert.ToInt32(values[4]);
            UserId = Convert.ToInt32(values[5]);
        }
    }
}

