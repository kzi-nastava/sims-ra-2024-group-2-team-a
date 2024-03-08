using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Location : BookingApp.Serializer.ISerializable
    {
        public int Id { get; set; }
        public string City {  get; set; }
        public string Country { get; set; }
        public Location() { }

        public Location(string city, string country) {
            City = city;
            Country = country;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            City = values[1];
            Country = values[2];
        }

        public string[] ToCSV()
        {
            string[] values = {Id.ToString(),City,Country };
            return values;
        }
    }
}