using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model {
    public enum ImportanceType
    {
        Level_1, Level_2, Level_3, Level_4, Level_5
    }

    public class Review : ISerializable, IIdentifiable
    {

        public int Id { get; set; } = 0;
        public int ReservationId { get; set; } = 0;
        public int GuestId { get; set; } = 0;
        public int OwnerId { get; set; } = 0;
        public int GuestCleannessGrade { get; set; } = 0;
        public int RuleFollowingGrade { get; set; } = 0;
        public string OwnerComment { get; set; } = "";
        public int AccommodationCleannessGrade { get; set; } = 0;
        public int OwnerCorrectnessGrade { get; set; } = 0;
        public string GuestComment { get; set; } = "";
        public bool RequiresRenovation { get; set; } = false;
        public ImportanceType Importance { get; set; } = ImportanceType.Level_1;
        public string RenovationComment { get; set; } = "";
        public List<string> AccommodationPhotos { get; set; } = new List<string>();

        public Review() { }

        public Review(int resId, int guestId, int ownerId)
        {
            ReservationId = resId;
            GuestId = guestId;
            OwnerId = ownerId;
        }

        public Review(int resId, int guestId, int ownerId, int guestCleannessGrade, int ruleFollowingGrade,
            string ownerComment, int accommodationCleannessGrade, int ownerCorrectnessGrade, string guestComment,
            bool requiresRenovation, ImportanceType importance, string renovationComment, List<string> accommodationPhotos)
        {
            ReservationId = resId;
            GuestId = guestId;
            OwnerId = ownerId;
            GuestCleannessGrade = guestCleannessGrade;
            RuleFollowingGrade = ruleFollowingGrade;
            OwnerComment = ownerComment;
            AccommodationCleannessGrade = accommodationCleannessGrade;
            OwnerCorrectnessGrade = ownerCorrectnessGrade;
            GuestComment = guestComment;
            RequiresRenovation = requiresRenovation;
            Importance = importance;
            RenovationComment = renovationComment;
            AccommodationPhotos.AddRange(accommodationPhotos);
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                ReservationId.ToString(),
                GuestId.ToString(),
                OwnerId.ToString(),
                GuestCleannessGrade.ToString(),
                RuleFollowingGrade.ToString(),
                OwnerComment,
                AccommodationCleannessGrade.ToString(),
                OwnerCorrectnessGrade.ToString(),
                GuestComment,
                RequiresRenovation.ToString(),
                Importance.ToString(),
                RenovationComment
            };

            csvValues = csvValues.Concat(AccommodationPhotos).ToArray();

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            GuestId = int.Parse(values[2]);
            OwnerId = int.Parse(values[3]);
            GuestCleannessGrade = int.Parse(values[4]);
            RuleFollowingGrade = int.Parse(values[5]);
            OwnerComment = values[6];
            AccommodationCleannessGrade = int.Parse(values[7]);
            OwnerCorrectnessGrade = int.Parse(values[8]);
            GuestComment = values[9];
            RequiresRenovation = bool.Parse(values[10]);
            Importance = (ImportanceType)Enum.Parse(typeof(ImportanceType), values[11]);
            RenovationComment = values[12];
            AccommodationPhotos.AddRange(values[13..]);
        }
    }
}
