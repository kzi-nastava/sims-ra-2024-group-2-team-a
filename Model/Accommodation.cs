using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xaml.Schema;

namespace BookingApp.Model
{
    public enum AccommodationType { apartment, house, hut}
    public class Accommodation: ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int LocationId { get; set; }

        public AccommodationType type;
        public int MaxGuestNumber { get; set; }
        public int MinReservationDays { get; set; }
        public int LastCancellationDay { get; set; }

        public int OwnerId { get; set; }

        public List<int> GuestId { get; set; }
        public List<string> ProfilePictures { get; set; }
        public Accommodation() {
            GuestId = new List<int>();
            ProfilePictures = new List<string>();
        }
        public Accommodation(string name,int locationId, AccommodationType accomodationType,
            int maxguestNumber, int minReservationDays, int cancellationDate, int ownerId ,List<string> profilePictures)
        {
            Name = name;
            LocationId = locationId;
            type = accomodationType;
            MaxGuestNumber = maxguestNumber;
            MinReservationDays = minReservationDays;
            LastCancellationDay = cancellationDate;
            OwnerId = ownerId;
            ProfilePictures = profilePictures;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { 
                Id.ToString(),
                Name,
                LocationId.ToString(),
                type.ToString(),
                MaxGuestNumber.ToString(),
                MinReservationDays.ToString(),
                LastCancellationDay.ToString(),
                OwnerId.ToString()
                };

            if (ProfilePictures != null)
            {
                foreach (string profilePicture in ProfilePictures)
                {
                    Array.Resize(ref csvValues,csvValues.Length+1);
                    csvValues[csvValues.Length-1] = profilePicture;
                }
            }

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = int.Parse(values[2]);
            Enum.TryParse(values[3], out type);
            MaxGuestNumber = Convert.ToInt32(values[4]);
            MinReservationDays = Convert.ToInt32(values[5]);
            LastCancellationDay = Convert.ToInt32(values[6]);
            OwnerId = Convert.ToInt32(values[7]);
            ProfilePictures.AddRange(values[8..]);
        }
}
}
