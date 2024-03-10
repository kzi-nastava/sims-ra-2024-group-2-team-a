using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model {
    public class Tour : BookingApp.Serializer.ISerializable {
        public int Id { get; set; }
        public Location Location { get; set; }
        public double Duration { get; set; }
        public string Language { get; set; }
        public int MaxTouristNumber { get; set; }
        public int CurrentTouristNumber { get; set; }
        public Tour() { }

        public void FromCSV(string[] values) {
            
        }

        public string[] ToCSV() {
            string[] values = { };
            return values;
        }
    }
}