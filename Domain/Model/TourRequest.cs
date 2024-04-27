using BookingApp.Serializer;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model {
    public enum TourRequestStatus { OnHold, Expired, Confirmed };
    public class TourRequest : ISerializable, IIdentifiable {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public TourRequestStatus Status { get; set; }
        public int PassengerNumber { get; set; }

        public TourRequest() {

        }

        /* public TourRequest(int locationId, string description, int languageId, DateOnly startDate, DateOnly endDate) {
            LocationId = locationId;
            Description = description;
            LanguageId = languageId;
            StartDate = startDate;
            EndDate = endDate;
            Status = TourRequestStatus.OnHold;           
        } */

        public TourRequest(TourRequestDTO tourRequest, int passengerNumber) {
            LocationId = tourRequest.LocationId;
            Description = tourRequest.Description;
            LanguageId = tourRequest.LanguageId;
            StartDate = tourRequest.StartDate;
            EndDate = tourRequest.EndDate;
            Status = TourRequestStatus.OnHold;
            PassengerNumber = passengerNumber;
        }

        public string[] ToCSV() {
            string[] csvValues = {
                Id.ToString(),
                TouristId.ToString(),
                LocationId.ToString(),
                Description,
                LanguageId.ToString(),
                StartDate.ToString("dd-MM-yyyy"),
                EndDate.ToString("dd-MM-yyyy"),
                Status.ToString(),
                PassengerNumber.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            LocationId = int.Parse(values[2]);
            Description = values[3];
            LanguageId = int.Parse(values[4]);
            StartDate = DateOnly.ParseExact(values[5], "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            EndDate = DateOnly.ParseExact(values[6], "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            Status = (TourRequestStatus)Enum.Parse(typeof(TourRequestStatus), values[7]);
            PassengerNumber = Convert.ToInt32(values[8]);
        }
    }
}
