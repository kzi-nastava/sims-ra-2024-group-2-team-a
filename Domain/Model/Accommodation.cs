using BookingApp.Serializer;
using System;
using System.Collections.Generic;

namespace BookingApp.Domain.Model {

    public enum AccommodationType { any, apartment, house, hut }

    public class Accommodation : ISerializable, IIdentifiable
    {

        public int Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public int LocationId { get; set; } = 0;
        public AccommodationType Type { get; set; } = AccommodationType.any;
        public int MaxGuestNumber { get; set; } = 0;
        public int MinReservationDays { get; set; } = 0;
        public int LastCancellationDay { get; set; } = 0;
        public int OwnerId { get; set; } = 0;
        public List<string> Pictures { get; set; } = new List<string>();

        public Accommodation() { }

        public Accommodation(string name, int locationId, AccommodationType accomodationType,
            int maxGuestNumber, int minReservationDays, int cancellationDate, int ownerId, List<string> pictures)
        {
            Name = name;
            LocationId = locationId;
            Type = accomodationType;
            MaxGuestNumber = maxGuestNumber;
            MinReservationDays = minReservationDays;
            LastCancellationDay = cancellationDate;
            OwnerId = ownerId;
            Pictures = pictures;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Name,
                LocationId.ToString(),
                Type.ToString(),
                MaxGuestNumber.ToString(),
                MinReservationDays.ToString(),
                LastCancellationDay.ToString(),
                OwnerId.ToString()
                };

            if (Pictures != null)
            {
                foreach (string pictures in Pictures)
                {
                    Array.Resize(ref csvValues, csvValues.Length + 1);
                    csvValues[csvValues.Length - 1] = pictures;
                }
            }

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = int.Parse(values[2]);
            Type = (AccommodationType)Enum.Parse(typeof(AccommodationType), values[3]);
            MaxGuestNumber = Convert.ToInt32(values[4]);
            MinReservationDays = Convert.ToInt32(values[5]);
            LastCancellationDay = Convert.ToInt32(values[6]);
            OwnerId = Convert.ToInt32(values[7]);
            Pictures.AddRange(values[8..]);
        }
    }
}
