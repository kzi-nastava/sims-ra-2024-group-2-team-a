using BookingApp.Serializer;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model {
    public class RequestPassenger : ISerializable, IIdentifiable {
        public int Id { get; set; }
        public int TourRequestId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int UserId { get; set; }

        public RequestPassenger() {

        }

        public RequestPassenger(int tourRequestId, PassengerDTO passengerDTO) {
            TourRequestId = tourRequestId;
            Name = passengerDTO.Name;
            Surname = passengerDTO.Surname;
            Age = passengerDTO.Age;
            UserId = passengerDTO.UserId;
        }

        public void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            TourRequestId = Convert.ToInt32(values[1]);
            Name = values[2];
            Surname = values[3];
            Age = Convert.ToInt32(values[4]);
            UserId = Convert.ToInt32(values[5]);
        }

        public string[] ToCSV() {
            string[] csvValues = { Id.ToString(), TourRequestId.ToString(), Name, Surname, Age.ToString(), UserId.ToString() };
            return csvValues;
        }
    }
}
