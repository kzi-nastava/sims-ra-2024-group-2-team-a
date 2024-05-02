using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model {

    public enum RenovationState {Pending, Finished, Cancelled }
    public class AccommodationRenovation : ISerializable, IIdentifiable {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }   
        public RenovationState RenovationState { get; set; }
        public string Description { get; set; } = string.Empty;

        public AccommodationRenovation() { }
        public AccommodationRenovation(int accommodationId, DateOnly startDate, DateOnly endDate,
            RenovationState renovationState, string desc) {
            AccommodationId = accommodationId;
            StartDate = startDate;
            EndDate = endDate;
            RenovationState = renovationState;
            Description = desc;
        }

        public void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            StartDate = DateOnly.ParseExact(values[2], "dd-MM-yyyy");
            EndDate = DateOnly.ParseExact(values[3], "dd-MM-yyyy");
            RenovationState = (RenovationState)Enum.Parse(typeof(RenovationState), values[4]);
            Description = values[5];
        }

        public string[] ToCSV() {
            string[] csvValues = {
                Id.ToString(),
                AccommodationId.ToString(),
                StartDate.ToString("dd-MM-yyyy"),
                EndDate.ToString("dd-MM-yyyy"),
                RenovationState.ToString(),
                Description
            };

            return csvValues;
        }
    }
}
