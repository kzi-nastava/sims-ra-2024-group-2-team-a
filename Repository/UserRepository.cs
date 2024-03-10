using BookingApp.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class UserRepository : IRepository<User>
    {
        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> _serializer;

        private List<User> _users;

        public UserRepository()
        {
            _serializer = new Serializer<User>();
            _users = _serializer.FromCSV(FilePath);
        }

        public List<User> GetAll() {
            throw new System.NotImplementedException();
        }

        public User Save(User item) {
            throw new System.NotImplementedException();
        }

        public bool Update(User item) {
            throw new System.NotImplementedException();
        }

        public bool Delete(User item) {
            throw new System.NotImplementedException();
        }

        public User GetById(int id) {
            throw new System.NotImplementedException();
        }

        public User GetByUsername(string username)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Username == username);
        }
    }
}
