using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model {
    public class Language : BookingApp.Serializer.ISerializable {
        public int Id { get; set; }
        public string Name { get; set; }

        public Language() { }

        public Language(string name) {
            Name = name;
        }

        public void FromCSV(string[] values) {
            Id = int.Parse(values[0]);
            Name = values[1];
        }

        public string[] ToCSV() {
            string[] values = { Id.ToString(), Name};
            return values;
        }
    }
}
