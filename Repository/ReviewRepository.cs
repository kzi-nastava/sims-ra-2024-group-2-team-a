using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository {
    public class ReviewRepository : Repository<Review> {
        public Review GetByReservationId(int id) {
            return _serializer.FromCSV().Find(x=> x.ReservationId == id);
        }
    }
}
