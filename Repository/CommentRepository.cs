using BookingApp.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BookingApp.Repository
{
    public class CommentRepository
    {
        private readonly Serializer<Comment> _serializer;

        private List<Comment> _comments;

        public CommentRepository()
        {
            _serializer = new Serializer<Comment>();
            _comments = _serializer.FromCSV();
        }

        public List<Comment> GetAll()
        {
            return _serializer.FromCSV();
        }

        public Comment Save(Comment comment)
        {
            comment.Id = NextId();
            _comments = _serializer.FromCSV();
            _comments.Add(comment);
            _serializer.ToCSV(_comments);
            return comment;
        }

        public void Delete(Comment comment)
        {
            _comments = _serializer.FromCSV();
            Comment founded = _comments.Find(c => c.Id == comment.Id);
            _comments.Remove(founded);
            _serializer.ToCSV(_comments);
        }

        public Comment Update(Comment comment)
        {
            _comments = _serializer.FromCSV();
            Comment current = _comments.Find(c => c.Id == comment.Id);
            int index = _comments.IndexOf(current);
            _comments.Remove(current);
            _comments.Insert(index, comment);       // keep ascending order of ids in file 
            _serializer.ToCSV(_comments);
            return comment;
        }
        public int NextId() {
            _comments = _serializer.FromCSV();
            if (_comments.Count < 1) {
                return 1;
            }
            return _comments.Max(c => c.Id) + 1;
        }

        public List<Comment> GetByUser(User user)
        {
            _comments = _serializer.FromCSV();
            return _comments.FindAll(c => c.User.Id == user.Id);
        }
    }
}
