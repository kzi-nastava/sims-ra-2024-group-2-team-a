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
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public Passenger() { }

        public Passenger(int id, int tourId, string name, string surname, int age) {
            Id = id;
            TourId = tourId;
            Name = name;
            Surname = surname;
            Age = age;
        }

        public string[] ToCSV() {
            string[] csvValues = { Id.ToString(), TourId.ToString(), Name, Surname, Age.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            Name = values[2];
            Surname = values[3];
            Age = Convert.ToInt32(values[4]);
        }
    }
}

