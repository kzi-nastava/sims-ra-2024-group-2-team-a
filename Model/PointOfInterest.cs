using BookingApp.Serializer;
using System;

namespace BookingApp.Model {
    public class PointOfInterest : ISerializable, IIdentifiable {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsChecked { get; set; }
        public int TourId { get; set; }
        public PointOfInterest() { }

        public PointOfInterest(int id, string name, string description, bool isChecked, int tourId) {
            Id = id;
            Name = name;
            Description = description;
            IsChecked = isChecked;
            TourId = tourId;
        }
        public PointOfInterest(string name, string description, bool isChecked, int tourId) {
            Name = name;
            Description = description;
            IsChecked = isChecked;
            TourId = tourId;
        }

        public string[] ToCSV() {
            string[] csvValues = {
                Id.ToString(),
                Name,
                Description,
                IsChecked.ToString(),
                TourId.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Description = values[2];
            IsChecked = bool.Parse(values[3]);
            TourId = Convert.ToInt32(values[4]);
        }
    }
}