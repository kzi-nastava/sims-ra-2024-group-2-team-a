using BookingApp.Serializer;
using BookingApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model {
    public class AccommodationStatistics : ISerializable, IIdentifiable {
        public int Id{ get; set;}
        public int AccommodationId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int ReservationsNum { get; set; }
        public int CancelledReservationsNum { get; set; }
        public int PostponedReservationsNum { get; set; }
        public int RenovationsRecommendedNum { get; set; }
        public int DaysReserved { get; set; }
        public AccommodationStatistics() {}
        public AccommodationStatistics(int accommodationId, int year, int month) {
            AccommodationId = accommodationId;
            Year = year;
            Month = month;
            ReservationsNum = 0;
            CancelledReservationsNum = 0;
            PostponedReservationsNum = 0;
            RenovationsRecommendedNum = 0;
            DaysReserved = 0;
        }
        public void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            Year = Convert.ToInt32(values[2]);
            Month = Convert.ToInt32(values[3]);
            ReservationsNum = Convert.ToInt32(values[4]);
            CancelledReservationsNum = Convert.ToInt32(values[5]);
            PostponedReservationsNum = Convert.ToInt32(values[6]);
            RenovationsRecommendedNum = Convert.ToInt32(values[7]);
            DaysReserved = Convert.ToInt32(values[8]);
        }

        public string[] ToCSV() {
            string[] csvValues = {
                Id.ToString(),
                AccommodationId.ToString(),
                Year.ToString(),
                Month.ToString(),
                ReservationsNum.ToString(),
                CancelledReservationsNum.ToString(),
                PostponedReservationsNum.ToString(),
                RenovationsRecommendedNum.ToString(),
                DaysReserved.ToString()
            };

            return csvValues;
        }
    }
}
