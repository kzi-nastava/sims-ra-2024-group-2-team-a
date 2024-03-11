using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model {
    public class Tour : BookingApp.Serializer.ISerializable {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxTouristNumber { get; set; }
        public List<PointOfInterest> PointOfInterests { get; set; }
        public List<DateTime> Beginnings { get; set; }
        public double Duration { get; set; }
        public int CurrentTouristNumber { get; set; }
        public List<string> ProfilePictures { get; set; }

        public Tour() { 
            ProfilePictures = new List<string>();
        }
        public Tour(int id, string name, int locationId, string description, string language, int maxTouristNumber, double duration, int currentTouristNumber, List<string> profilePictures) {
            Id = id;
            Name = name;
            LocationId = locationId;
            Description = description;
            Language = language;
            MaxTouristNumber = maxTouristNumber;
            Duration = duration;
            CurrentTouristNumber = currentTouristNumber;
            ProfilePictures = profilePictures;
        }

        public string[] ToCSV() {
            string[] csvValues = {
                Id.ToString(),
                Name,
                LocationId.ToString(),
                Description,
                Language,
                MaxTouristNumber.ToString(),
                Duration.ToString(),
                CurrentTouristNumber.ToString()
                };

             if (ProfilePictures != null) {
                foreach (string profilePicture in ProfilePictures) {
                    Array.Resize(ref csvValues, csvValues.Length + 1);
                    csvValues[csvValues.Length - 1] = profilePicture;
                }
            }

            return csvValues;
        }

        public void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = int.Parse(values[2]);
            Description = values[3];
            Language = values[4];
            MaxTouristNumber = Convert.ToInt32(values[5]);
            Duration = Convert.ToDouble(values[6]);
            CurrentTouristNumber = Convert.ToInt32(values[7]);
            ProfilePictures.AddRange(values[8..]);
        }
    }
}