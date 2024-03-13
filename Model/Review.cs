using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using BookingApp.Serializer;

namespace BookingApp.Model {
    public class Review : Serializer.ISerializable {
        public Review() {
            Id = 0;
            ReservationId = 0;
            GuestId = 0;
            OwnerId = 0;
            GuestCleannessGrade = 0;
            GuestRuleFollowingGrade = 0;
            OwnerComment = "";
            AccommodationCleannessGrade = 0;
            OwnerCorrectnessGrade = 0;
            GuestComment = "";
        }
        public Review(int resId, int guestId,int ownerId) {
            ReservationId = resId;
            GuestId = guestId;
            OwnerId = ownerId;
            GuestCleannessGrade = 0;
            GuestRuleFollowingGrade = 0;
            OwnerComment = "";
            AccommodationCleannessGrade = 0;
            OwnerCorrectnessGrade = 0;
            GuestComment = "";
        }

        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int GuestId { get; set; }
        public int OwnerId { get; set; }
        public int GuestCleannessGrade { get; set; }
        public int GuestRuleFollowingGrade { get; set; }
        public string OwnerComment { get; set; }
        public int AccommodationCleannessGrade { get; set; }
        public int OwnerCorrectnessGrade { get; set; }
        public string GuestComment { get; set; }

        public string[] ToCSV() {
            string[] csvValues = {
                Id.ToString(),
                ReservationId.ToString(),
                GuestId.ToString(),
                OwnerId.ToString(),
                GuestCleannessGrade.ToString(),
                GuestRuleFollowingGrade.ToString(),
                OwnerComment,
                AccommodationCleannessGrade.ToString(),
                OwnerCorrectnessGrade.ToString(),
                GuestComment
            };

            return csvValues;
        }

        public void FromCSV(string[] values) {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            GuestId = int.Parse(values[2]);
            OwnerId = int.Parse(values[3]);
            GuestCleannessGrade = int.Parse(values[4]);
            GuestRuleFollowingGrade = int.Parse(values[5]);
            OwnerComment = values[6];
            AccommodationCleannessGrade = int.Parse(values[7]);
            OwnerCorrectnessGrade = int.Parse(values[8]);
            GuestComment = values[9];
        }
    }
}
