using BookingApp.Serializer;
using System;

namespace BookingApp.Domain.Model {
    public class Comment : ISerializable, IIdentifiable
    {
        public int Id { get; set; }
        public int ForumId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public int ReportsNum { get; set; }
        public DateTime CreationTime { get; set; }
        public Comment() { }
        public Comment(DateTime creationTime, string text, int userId, int forumId)
        {
            CreationTime = creationTime;
            Text = text;
            UserId = userId;
            ForumId = forumId;
            ReportsNum = 0;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { 
                Id.ToString(),
                UserId.ToString(),
                ForumId.ToString(),
                Text,
                ReportsNum.ToString(),
                CreationTime.ToString("dd-MM-yyyy HH:mm:ss")
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            ForumId = Convert.ToInt32(values[2]);
            Text = values[3];
            ReportsNum = Convert.ToInt32(values[4]);
            CreationTime = DateTime.ParseExact(values[5], "dd-MM-yyyy HH:mm:ss", null);
        }
    }
}
