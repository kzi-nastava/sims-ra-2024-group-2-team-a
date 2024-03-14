using System;
using System.Collections.Generic;

namespace BookingApp.Model {
    public enum LanguageState { Serbian = 0, English, Russian, Spanish, German}
    public class Tour : BookingApp.Serializer.ISerializable {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public LanguageState Language;
        public int MaxTouristNumber { get; set; }
        public double Duration { get; set; }
        public int CurrentTouristNumber { get; set; }
        public DateTime Beggining { get; set; }
        public int GuideId { get; set; }
        public List<string> ProfilePictures { get; set; }
        public List<PointOfInterest> PointOfInterests { get; set; }


        public Tour() {
            ProfilePictures = new List<string>();
            PointOfInterests = new List<PointOfInterest>(); 
        }
        public Tour(int id, string name, int locationId, string description, LanguageState language, int maxTouristNumber, double duration, int currentTouristNumber, DateTime beggining, int guideId, List<string> profilePictures) {

            Id = id;
            Name = name;
            LocationId = locationId;
            Description = description;
            Language = language;
            MaxTouristNumber = maxTouristNumber;
            Duration = duration;
            CurrentTouristNumber = currentTouristNumber;
            Beggining = beggining;
            GuideId = guideId;
            ProfilePictures = profilePictures;
        }

        public string[] ToCSV() {
            string[] csvValues = {
                Id.ToString(),
                Name,
                LocationId.ToString(),
                Description,
                Language.ToString(),
                MaxTouristNumber.ToString(),
                Duration.ToString(),
                CurrentTouristNumber.ToString(),
                Beggining.ToString(),
                GuideId.ToString(),
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
            Enum.TryParse(values[4], out Language);
            MaxTouristNumber = Convert.ToInt32(values[5]);
            Duration = Convert.ToDouble(values[6]);
            CurrentTouristNumber = Convert.ToInt32(values[7]);
            Beggining = Convert.ToDateTime(values[8]);
            GuideId = Convert.ToInt32(values[9]);
            ProfilePictures.AddRange(values[10..]);
        }
    }
}
