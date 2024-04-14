using System;

namespace BookingApp.Model {

    public class Tourist : User {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public Tourist() { }

        public Tourist(string username, string password) : base(username, password) {
            Category = UserCategory.Tourist;
        }

        public string[] ToCSV() {
            string[] csvValues = { Id.ToString(), Name, Surname, Age.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values) {
            base.FromCSV(values);
            Name = values[0];
            Surname = values[1];
            Age = Convert.ToInt32(values[2]);
        }
    }
}
