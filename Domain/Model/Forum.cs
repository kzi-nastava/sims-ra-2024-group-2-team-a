using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Domain.Model {
    public class Forum : ISerializable, IIdentifiable {
        public int Id { set; get; }
        public int LocationId { get; set; }
        public int CreatorId { get; set; }
        public int GuestCommentNum { get; set; }
        public int OwnerCommentNum { get; set; }
        public bool IsUsefull { get; set; }
        public bool IsClosed { get; set; }
        public string Title { get; set; }

        //smart methods
        public void UpgradeToUsefull() {
            if(GuestCommentNum > 20 && OwnerCommentNum > 10)
                IsUsefull = true;
        }
        public void Close() {
            IsClosed = true;
        }

        //constructors and from/to csv
        public Forum() {
        }
        public Forum(int creatorId, int locationId, string title) {
            LocationId = locationId;
            CreatorId = creatorId;
            GuestCommentNum = 0;
            OwnerCommentNum = 0;
            IsClosed = false;
            IsUsefull = false;
            Title = title;
        }
        public void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            LocationId = Convert.ToInt32(values[1]);
            CreatorId = Convert.ToInt32(values[2]);
            GuestCommentNum = Convert.ToInt32(values[3]);
            OwnerCommentNum = Convert.ToInt32(values[4]);
            IsClosed = Boolean.Parse(values[5]);
            IsUsefull = Boolean.Parse(values[6]);
            Title = values[7];
        }

        public string[] ToCSV() {
            string[] csvValues = {
                Id.ToString(),
                LocationId.ToString(),
                CreatorId.ToString(),
                GuestCommentNum.ToString(),
                OwnerCommentNum.ToString(),
                IsClosed.ToString(),
                IsUsefull.ToString(),
                Title
                };

            return csvValues;
        }
    }
}
