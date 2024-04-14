using BookingApp.Serializer;
using System;

namespace BookingApp.Domain.Model {
    public class Owner : ISerializable, IIdentifiable
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public bool IsSuper { get; set; }
        public double AverageGrade { get; set; }
        public Owner()
        {
            IsSuper = false;
            AverageGrade = 0;
        }

        public Owner(int userId, bool super, double avg)
        {
            UserId = userId;
            IsSuper = super;
            AverageGrade = avg;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), UserId.ToString(), IsSuper.ToString(), AverageGrade.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            IsSuper = Convert.ToBoolean(values[2]);
            AverageGrade = Convert.ToDouble(values[3]);
        }
    }
}
