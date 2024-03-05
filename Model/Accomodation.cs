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
    public enum AccomodationType { apartment, house, hut}
    public class Accomodation: ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Location Location { get; set; }
        public int LocationId { get; set; }

        public AccomodationType type;
        public int MaxGuestNumber { get; set; }
        public int MinReservationDays { get; set; }
        public int LastCancellationDay { get; set; }
        public string[] ProfilePictures { get; set; }
        public Accomodation() {
        }
        public Accomodation(string name, Location loc, AccomodationType accomodationType,
            int maxguestNumber, int minReservationDays, int cancellationDate, string[] profilePictures)
        {
            Name = name;
            Location = loc;
            LocationId = loc.Id;
            type = accomodationType;
            MaxGuestNumber = maxguestNumber;
            MinReservationDays = minReservationDays;
            LastCancellationDay = cancellationDate;
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
                };

            if (ProfilePictures != null)
            {
                foreach (string profilePicture in ProfilePictures)
                {
                    csvValues.Append(profilePicture);
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

            for (int i=8;i<values.Length;i++)
            {
                ProfilePictures.Append(values[i]);
            }
        }
}
}
