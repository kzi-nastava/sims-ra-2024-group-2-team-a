using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ISerializable = BookingApp.Serializer.ISerializable;

namespace BookingApp.Domain.Model {
    public class ComplexTourRequest : ISerializable, IIdentifiable {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public TourRequestStatus Status { get; set; } = TourRequestStatus.OnHold;
        public ComplexTourRequest() { }
        public ComplexTourRequest(int touristId) { 
            TouristId = touristId;
            Status = TourRequestStatus.OnHold;
        }
        public string[] ToCSV() {
            string[] csvValues = {
                Id.ToString(),
                TouristId.ToString(),
                Status.ToString()
            };

            return csvValues;
        }
        public void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            Status = (TourRequestStatus)Enum.Parse(typeof(TourRequestStatus), values[2]);
        }
    }
}
