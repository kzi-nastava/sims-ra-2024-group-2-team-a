using System;
using System.Globalization;

namespace BookingApp.Domain.Model {

    public class Tourist : User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public Tourist() { }

        public Tourist(User user) : base(user)
        {
            Category = UserCategory.Tourist;
        }

        public Tourist(int id, string name, string surname, DateOnly dateOfBirth) {
            Id = id;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
        }

        public override string[] ToCSV()

        {
            string[] csvValues = { Id.ToString(), Name, Surname, DateOfBirth.ToString("dd-MM-yyyy") };
            return csvValues;
        }


        public override void FromCSV(string[] values)

        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Surname = values[2];
            DateOfBirth = DateOnly.ParseExact(values[3], "dd-MM-yyyy", CultureInfo.InvariantCulture);
        }
    }
}
