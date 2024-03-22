using BookingApp.DTO;
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

        public List<Review> GetByOwnerId(int ownerId) {
            return _serializer.FromCSV().Where(review => review.OwnerId == ownerId).ToList();
        }
        public void GradeOwner(ReviewDTO reviewDTO) {
            
            
            // prostor za bata kuleta
            // veoma je bitno da se pozove tvoj save pre moje logike
            // da se uracuna i ta najnovija ocena
            OwnerRepository ownerRepository = new OwnerRepository();
            ownerRepository.AdjustSuperOwner(reviewDTO.OwnerId);
        }

    }
}
