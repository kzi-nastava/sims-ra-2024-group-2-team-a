using BookingApp.Serializer;
using System;

namespace BookingApp.Domain.Model {
    public enum UserCategory { Owner, Guest, Guide, Tourist }

    public class User : ISerializable, IIdentifiable
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserCategory Category { get; set; }

        public User() { }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Category.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Enum.TryParse(values[3], out UserCategory tmp);
            Category = tmp;
        }
    }
}
