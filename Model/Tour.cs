using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BookingApp.Model {

    public class Tour : ISerializable, IIdentifiable {

        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; }
        public int MaxTouristNumber { get; set; }
        public double Duration { get; set; }
        public int CurrentTouristNumber { get; set; }
        public DateTime Beggining { get; set; }
        public bool IsFinished {  get; set; }
        public int GuideId { get; set; }
        public List<string> ProfilePictures { get; set; }
        public List<PointOfInterest> PointOfInterests { get; set; }


        public Tour() {
            ProfilePictures = new List<string>();
            PointOfInterests = new List<PointOfInterest>(); 
        }

        public Tour(int id, string name, int locationId, string description, int languageId, int maxTouristNumber, double duration, int currentTouristNumber, DateTime beggining, bool isFinished, int guideId, List<string> profilePictures) {
            Id = id;
            Name = name;
            LocationId = locationId;
            Description = description;
            LanguageId = languageId;
            MaxTouristNumber = maxTouristNumber;
            Duration = duration;
            CurrentTouristNumber = currentTouristNumber;
            Beggining = beggining;
            IsFinished = isFinished;
            GuideId = guideId;
            ProfilePictures = profilePictures;
        }
        public Tour(string name, int locationId, string description, int languageId, int maxTouristNumber, double duration, int currentTouristNumber, DateTime beggining, bool isFinished, int guideId, List<string> profilePictures) {

            Name = name;
            LocationId = locationId;
            Description = description;
            LanguageId = languageId;
            MaxTouristNumber = maxTouristNumber;
            Duration = duration;
            CurrentTouristNumber = currentTouristNumber;
            Beggining = beggining;
            IsFinished = isFinished;
            GuideId = guideId;
            ProfilePictures = profilePictures;
        }

        public string[] ToCSV() {
            string[] csvValues = {
                Id.ToString(),
                Name,
                LocationId.ToString(),
                Description,
                LanguageId.ToString(),
                MaxTouristNumber.ToString(),
                Duration.ToString(),
                CurrentTouristNumber.ToString(),
                Beggining.ToString("dd-MM-yyyy HH:mm"),
                IsFinished.ToString(),
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
            LanguageId = int.Parse(values[4]);
            MaxTouristNumber = Convert.ToInt32(values[5]);
            Duration = Convert.ToDouble(values[6]);
            CurrentTouristNumber = Convert.ToInt32(values[7]);
            CultureInfo provider = CultureInfo.InvariantCulture;
            Beggining = DateTime.ParseExact(values[8], "dd-MM-yyyy HH:mm", provider);
            IsFinished = bool.Parse(values[9]);
            GuideId = Convert.ToInt32(values[10]);
            ProfilePictures.AddRange(values[11..]);
        }
    }
}
