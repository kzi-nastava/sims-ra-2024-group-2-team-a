using BookingApp.DTO;
using BookingApp.Serializer;
using System;

namespace BookingApp.Model {

    public class Passenger : ISerializable, IIdentifiable {

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

        public Passenger(int tourId, PassengerDTO passengerDTO) {
            TourReservationId = tourId;
            Name = passengerDTO.Name;
            Surname = passengerDTO.Surname;
            Age = passengerDTO.Age;
            UserId = passengerDTO.UserId;
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

