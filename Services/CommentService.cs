using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services {
    public class CommentService {

        private readonly ICommentRepository _commentRepository;

        private AccommodationReservationService _reservationService;
        public CommentService(ICommentRepository commentRepo) {
            _commentRepository = commentRepo;
        }

        public void InjectServices(AccommodationReservationService reservationService) {
            _reservationService = reservationService;
        }

        public void Save(Comment comment) {
            _commentRepository.Save(comment);
        }
        public List<Comment> GetAll() {
            return _commentRepository.GetAll();
        }

        public void Update(Comment comment) {
            _commentRepository.Update(comment);
        }

        public Comment GetById(int id) {
            return _commentRepository.GetById(id); 
        }
        public List<Comment> GetByForumId(int forumId) {
            return this.GetAll().Where(x => x.ForumId == forumId).OrderBy(x => x.CreationTime).ToList();
        }
        public bool IsCommentReportable(int guestId, DateTime dateTime, int locationId) {
            if(_reservationService.WasVisitedByGuest(guestId, dateTime,locationId)) {
                return false;
            }

            return true;
        }

        public void ReportComment(int commentId) {
            Comment comment = this.GetById(commentId);

            comment.ReportsNum++;

            this.Update(comment);
        }

    }
}
