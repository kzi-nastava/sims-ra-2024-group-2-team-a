using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model {
    public class VisitedTour : ISerializable, IIdentifiable {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public DateTime Date { get; set; }

        public VisitedTour() { }

        public VisitedTour(int touristId, DateTime date) { 
            TouristId = touristId;
            Date = date;
        }

        public void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            CultureInfo provider = CultureInfo.InvariantCulture;
            Date = DateTime.ParseExact(values[2], "dd-MM-yyyy HH:mm", provider);
        }

        public string[] ToCSV() {
            string[] values = { Id.ToString(), TouristId.ToString(), Date.ToString("dd-MM-yyyy HH:mm") };
            return values;
        }
    }
}
