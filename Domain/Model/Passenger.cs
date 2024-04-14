using BookingApp.Serializer;
using BookingApp.WPF.DTO;
using System;

namespace BookingApp.Domain.Model {

    public class Passenger : ISerializable, IIdentifiable
    {

        public int Id { get; set; }
        public int TourReservationId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int JoinedPointOfInterestId { get; set; }
        public int UserId { get; set; }

        public Passenger() { }

        public Passenger(int id, int tourReservationId, string name, string surname, int age, int joinedId, int userId)
        {
            Id = id;
            TourReservationId = tourReservationId;
            Name = name;
            Surname = surname;
            Age = age;
            JoinedPointOfInterestId = joinedId;
            UserId = userId;
        }

        public Passenger(int tourId, PassengerDTO passengerDTO)
        {
            TourReservationId = tourId;
            Name = passengerDTO.Name;
            Surname = passengerDTO.Surname;
            Age = passengerDTO.Age;
            JoinedPointOfInterestId = -1; //means not joined yet
            UserId = passengerDTO.UserId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TourReservationId.ToString(), Name, Surname, Age.ToString(), JoinedPointOfInterestId.ToString(), UserId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourReservationId = Convert.ToInt32(values[1]);
            Name = values[2];
            Surname = values[3];
            Age = Convert.ToInt32(values[4]);
            JoinedPointOfInterestId = Convert.ToInt32(values[5]);
            UserId = Convert.ToInt32(values[6]);
        }
    }
}

