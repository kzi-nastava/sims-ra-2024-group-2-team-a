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
        public CommentService(ICommentRepository commentRepo) {
            _commentRepository = commentRepo;
        }

        public void InjectServices() {
            
        }

        public List<Comment> GetAll() {
            return _commentRepository.GetAll();
        }
        public List<Comment> GetByForumId(int forumId) {
            return this.GetAll().Where(x => x.ForumId == forumId).OrderBy(x => x.CreationTime).ToList();
        }

    }
}
