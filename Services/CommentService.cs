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

        private ForumService _forumService;

        private UserService _userService;

        public CommentService(ICommentRepository commentRepo) {
            _commentRepository = commentRepo;
        }

        public void InjectServices(AccommodationReservationService reservationService, ForumService forumService, UserService userService) {
            _reservationService = reservationService;
            _forumService = forumService;
            _userService = userService;
        }

        public void Save(Comment comment) {
            _commentRepository.Save(comment);
            Forum f = _forumService.GetById(comment.ForumId);

            UserCategory u = _userService.GetById(comment.UserId).Category;

            if(u == UserCategory.Guest) {
                f.GuestCommentNum++;
            }
            else {
                f.OwnerCommentNum++;
            }

            f.UpgradeToUsefull();

            _forumService.Update(f);
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
        public void SaveOwnerComment(Comment comment) {
            _forumService.IncreaseOwnerComment(comment.ForumId);

            this.Save(comment);
        }

    }
}
