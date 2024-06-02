using BookingApp.Serializer;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model {
    public enum TourRequestStatus { OnHold, Expired, Accepted };
    public class TourRequest : ISerializable, IIdentifiable {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int GuideId { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public TourRequestStatus Status { get; set; }
        public int PassengerNumber { get; set; }
        public int ComplexTourId { get; set; }

        public TourRequest() {

        }

        public TourRequest(int id, int touristId, int guideId, int locationId, string description, int languageId, DateOnly startDate, DateOnly endDate, TourRequestStatus status, int passengerNumber, int complexTourId) {
            Id = id;
            TouristId = touristId;
            GuideId = guideId;
            LocationId = locationId;
            Description = description;
            LanguageId = languageId;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            PassengerNumber = passengerNumber;
            ComplexTourId = complexTourId;
        }

        public TourRequest(TourRequestDTO tourRequest, int complexTourId) {
            TouristId = tourRequest.TouristId;
            GuideId = tourRequest.GuideId;
            LocationId = tourRequest.Location.Id;
            Description = tourRequest.Description;
            LanguageId = tourRequest.Language.Id;
            StartDate = tourRequest.StartDate;
            EndDate = tourRequest.EndDate;
            Status = TourRequestStatus.OnHold;
            PassengerNumber = tourRequest.PassengerNumber;
            ComplexTourId = complexTourId;
        }

        public string[] ToCSV() {
            string[] csvValues = {
                Id.ToString(),
                TouristId.ToString(),
                GuideId.ToString(),
                LocationId.ToString(),
                Description,
                LanguageId.ToString(),
                StartDate.ToString("dd-MM-yyyy"),
                EndDate.ToString("dd-MM-yyyy"),
                Status.ToString(),
                PassengerNumber.ToString(),
                ComplexTourId.ToString(),
            };

            return csvValues;
        }

        public void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            GuideId = Convert.ToInt32(values[2]);
            LocationId = int.Parse(values[3]);
            Description = values[4];
            LanguageId = int.Parse(values[5]);
            StartDate = DateOnly.ParseExact(values[6], "dd-MM-yyyy", CultureInfo.InvariantCulture);
            EndDate = DateOnly.ParseExact(values[7], "dd-MM-yyyy", CultureInfo.InvariantCulture);
            Status = (TourRequestStatus)Enum.Parse(typeof(TourRequestStatus), values[8]);
            PassengerNumber = Convert.ToInt32(values[9]);
            ComplexTourId = int.Parse(values[10]);
        }
    }
}
