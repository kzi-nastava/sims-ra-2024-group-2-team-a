using System;

namespace BookingApp.Domain.Model {

    public class Tourist : User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public Tourist() { }

        public Tourist(string username, string password) : base(username, password)
        {
            Category = UserCategory.Tourist;
        }

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public string[] ToCSV()
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        {
            string[] csvValues = { Id.ToString(), Name, Surname, Age.ToString() };
            return csvValues;
        }

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public void FromCSV(string[] values)
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        {
            base.FromCSV(values);
            Name = values[0];
            Surname = values[1];
            Age = Convert.ToInt32(values[2]);
        }
    }
}
