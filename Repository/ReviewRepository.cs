using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository {
    public class ReviewRepository : IRepository<Review> {

        private readonly Serializer<Review> _serializer;

        private List<Review> _reviews;

        public ReviewRepository() {
            _serializer = new Serializer<Review>();
            _reviews = _serializer.FromCSV();
        }
        public bool Delete(Review review) {
            _reviews = _serializer.FromCSV();
            Review? found = _reviews.Find(c => c.Id == review.Id);
            if (found == null) {
                return false;
            }
            _reviews.Remove(found);
            _serializer.ToCSV(_reviews);
            return true;
        }

        public List<Review> GetAll() {
            return _serializer.FromCSV();
        }

        public Review GetById(int id) {
            _reviews = _serializer.FromCSV();
            return _reviews.Find(x => x.Id == id);
        }

        public Review Save(Review review) {
            review.Id = NextId();
            _reviews = _serializer.FromCSV();
            _reviews.Add(review);
            _serializer.ToCSV(_reviews);
            return review;
        }

        public bool Update(Review review) {
            _reviews = _serializer.FromCSV();
            Review current = _reviews.Find(c => c.Id == review.Id);
            int index = _reviews.IndexOf(current);
            if (current == null) {
                return false;
            }
            _reviews.Remove(current);
            _reviews.Insert(index, review);       // keep ascending order of ids in file 
            _serializer.ToCSV(_reviews);
            return true;
        }

        private int NextId() {
            _reviews = _serializer.FromCSV();
            if (_reviews.Count < 1) {
                return 1;
            }
            return _reviews.Max(c => c.Id) + 1;
        }

        public Review GetByReservationId(int id) {
            return _serializer.FromCSV().Find(x=> x.ReservationId == id);
        }

    }
}
